using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMEmu
{
    public class Session
    {
        public SocketServer ws;
        public static Dictionary<string, byte[]> SptFiles = new Dictionary<string, byte[]>();
        public static int MAX_PAYLOAD = 10248;
        public static byte[] _data = new byte[MAX_PAYLOAD]; // Max payload
        public static bool flag = false;
        public static List<byte[]> ReplyStack = new List<byte[]>();
        public static Form1 Main;
        public static short port;

        public Session(Form1 mainForm, short _port)
        {
            Main = mainForm;
            port = _port;
        }

        public void Start()
        {
            this.LoadData();

            Main.UpdateLoadingText("Starting SocketServer");
            ws = new SocketServer(port);
            ws.OnUpdateStatus += this.onState;
            Main.UpdateLoadingText("Finished loading");

            Thread.Sleep(1000);
            Main.UpdateLoadingText("");
            ws.Start();
        }

        public void LoadData()
        {
            SptFiles.Clear();

            Console.WriteLine("Loading Channel1.spt");
            Main.UpdateLoadingText("Loading Channel1.spt");
            byte[] Channel1 = File.ReadAllBytes(Application.StartupPath + @"\Spt\Channel1.Spt");
            SptFiles.Add("Channel1", Channel1);

            Console.WriteLine("Loading MusicList.spt");
            Main.UpdateLoadingText("Loading MusicList.spt");
            byte[] MusicList = File.ReadAllBytes(Application.StartupPath + @"\Spt\MusicList.Spt");
            SptFiles.Add("MusicList", MusicList);

            Console.WriteLine("Loading D007.spt");
            Main.UpdateLoadingText("Loading D007.spt");
            byte[] D007 = File.ReadAllBytes(Application.StartupPath + @"\Spt\D007.Spt");
            SptFiles.Add("D007", D007);

            Console.WriteLine("Loading D207.spt");
            Main.UpdateLoadingText("Loading D207.spt");
            byte[] D207 = File.ReadAllBytes(Application.StartupPath + @"\Spt\D207.Spt");
            SptFiles.Add("D207", D207);
        }

        private void onState(Object o, TCPData e)
        {
            _data = new byte[MAX_PAYLOAD]; // ZeroMemory

            SocketState state = e.State;
            ushort cmd = BitConverter.ToUInt16(e.Buffer, 2);

            byte[] length1 = new byte[2];
            Buffer.BlockCopy(state.Buffer, 0, length1, 0, 2);
            ushort length = BitConverter.ToUInt16(length1, 0);

            Console.WriteLine("[ DEBUG ] Got " + cmd + " which " + Convert.ToString(cmd, 16));
            Console.WriteLine();

            switch (cmd)
            {
                case 0x03f1:
                    {
                        Console.WriteLine("A client logged in!");
                        byte[] packets = new byte[]
                        {
                            0x37, 0x00, 0xf2, 0x03, 0x02, 0x00, 0x00, 0x5d,
                            0xfe, 0xda, 0xad, 0xf5, 0x7f, 0x6b, 0x0e, 0x49,
                            0x2c, 0x3b, 0xba, 0x56, 0x17, 0xbb, 0x8b, 0x4c,
                            0x1d, 0x07, 0x28, 0x80, 0xd2, 0x51, 0x0c, 0xda,
                            0x54, 0x4a, 0xd1, 0x50, 0x35, 0x61, 0xa8, 0xfe,
                            0x67, 0xb5, 0xaa, 0xe1, 0x8b, 0x5d, 0x7c, 0x7b,
                            0x2a, 0xac, 0x22, 0xc3, 0x02, 0xf8, 0x1e
                        };

                        Write(state, packets);
                        break;
                    }

                case 0x03f3:
                    {
                        Console.WriteLine("A client login again!");
                        byte[] packets = new byte[]
                        {
                            0x37, 0x00, 0xf4, 0x03, 0x40, 0xba, 0x11, 0x36,
                            0x84, 0x0d, 0x40, 0x7b, 0x78, 0x64, 0x2a, 0xc9,
                            0xc5, 0x19, 0xcc, 0xaa, 0x7d, 0xb1, 0x65, 0x3b,
                            0x70, 0x1e, 0x6c, 0x18, 0x58, 0x0f, 0x05, 0x22,
                            0xd8, 0x08, 0xc8, 0xd7, 0x1c, 0x15, 0x36, 0x84,
                            0x0d, 0x40, 0x7b, 0x78, 0x64, 0x2a, 0xc9, 0xc5,
                            0x19, 0xcc, 0xaa, 0x7d, 0xb1, 0x65, 0xef
                        };

                        Write(state, packets);
                        break;
                    }

                case 0xfff0:
                    {
                        Console.WriteLine("A logout or idle request?");
                        ws.Close(state);
                        break;
                    }

                case 0x03ec:
                    {
                        Console.WriteLine("Something after login!");
                        byte[] packets =
                        {
                            0x10, 0x00, 0xed, 0x03, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x87, 0x53, 0x00, 0x00
                        };

                        Write(state, packets);
                        break;
                    }

                case 0x03ea:
                    {
                        Console.WriteLine("Getting channel details");
                        int count = Copy(_data, SptFiles["Channel1"], 0);
                        DoWrite(state, count);
                        break;
                    }

                case 0x0fbe:
                    {
                        Console.WriteLine("MusicList");
                        int count = Copy(_data, SptFiles["MusicList"], 6);
                        DoWrite(state, count);
                        break;
                    }

                case 0x07d0:
                    {
                        Console.WriteLine("D007");
                        int count = Copy(_data, SptFiles["D007"], 20);
                        DoWrite(state, count);
                        break;
                    }

                case 0x07d2:
                    {
                        Console.WriteLine("Entering lobby");
                        byte[] packets = new byte[]
                        {
                            0x2e, 0x0, 0xdd, 0x07, // Byte header
                            0x8c, 0x6e, 0x3f, 0x8f, 0xc1, 0x91, // (news), GBIC (Chinese simplified) encoded
                            0xa7, 0x0, // String terminator
                            0x4f, 0x32, 0x2d, 0x4a, 0x41, 0x4d, 0x20, 0x45, 0x6d, 0x75, 0x6c, // O2-JAM, Server Emulator
                            0x61, 0x74, 0x6f, 0x72, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
                            0x0, 0x0 // Seperator
                        }; // I wanna use ASCII too like in https://github.com/djask/o2jam-server-emu/blob/master/O2ServerEmu/session.cpp#L124 but ASCII encoder return wrong byte[]

                        Buffer.BlockCopy(packets, 0, _data, 0, packets[0]);
                        int count = Copy(_data, SptFiles["D207"], 20, packets.Length);


                        DoWrite(state, count + packets[0]);
                        break;
                    }

                case 0x07dc:
                    {
                        Console.WriteLine("Just a message, skip it to next packets!");
                        ws.ReadAgain(state);
                        break;
                    }

                case 0x13a4:
                    {
                        Console.WriteLine("No idea lol");
                        byte[] packets = new byte[]
                        {
                            0x08, 0x00, 0xa5, 0x13, 0xb3, 0x11, 0x01, 0x00
                        };

                        Write(state, packets);
                        break;
                    }

                case 0x07d4:
                    {
                        byte[] roomName = new byte[4];
                        Buffer.BlockCopy(state.Buffer, 4, roomName, 0, 4);

                        Console.WriteLine("Room creation: {0}", Encoding.UTF8.GetString(roomName));
                        byte[] packets = new byte[]
                        {
                            0x0d, 0x00, 0xd6, 0x07, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x00
                        };

                        Write(state, packets);
                        flag = true;
                        break;
                    }

                case 0x07e8:
                    {
                        Console.WriteLine("Massive payload it says, but whatever");
                        byte[] packets = new byte[]
                        {
                            0x04, 0x00, 0xe9, 0x07
                        };
                        Write(state, packets);
                        break;
                    }

                case 0x03e8:
                    {
                        Console.WriteLine("Channel login!");
                        byte[] packets = new byte[]
                        {
                            0x08, 0x00, 0xe9, 0x03, 0x00, 0x00, 0x00, 0x00
                        };

                        Write(state, packets);
                        break;
                    }

                case 0x03ef:
                    {
                        Console.WriteLine("Got login credentials");
                        byte[] packets = new byte[]
                        {
                            0x0c, 0x00, 0xf0, 0x03, 0x00, 0x00, 0x00, 0x00,
                            0xee, 0x60, 0x01, 0x00
                        };

                        Write(state, packets);
                        break;
                    }

                case 0x0fa0:
                    {
                        Console.WriteLine("Song select");
                        state.Buffer[2] = 0xa1;

                        if (!flag)
                        {
                            _data = state.Buffer;
                            DoWrite(state, _data[0]);
                        }
                        else
                        {
                            byte[] cp = new byte[length];
                            Buffer.BlockCopy(state.Buffer, 0, cp, 0, length);
                            ReplyStack.Add(cp);
                            ws.ReadAgain(state);
                        }
                        break;
                    }

                case 0x0fa4:
                    {
                        Console.WriteLine("Preparing room stuff, waiting to load...");
                        byte[] packets = new byte[] { 0x06, 0x00, 0xa5, 0x0f, 0x00, 0x00 };
                        ReplyStack.Add(packets);
                        ws.ReadAgain(state);

                        break;
                    }

                case 0x0fb7:
                    {
                        Console.WriteLine("Finished loading room, sending all replied buffer");
                        byte[] packets = new byte[] { 0x09, 0x00, 0xb8, 0x0f, 0x01, 0x00, 0x00, 0x00, 0x00 };
                        Write(state, packets);

                        while (ReplyStack.Count != 0)
                        {
                            IEnumerable<byte[]> repl_ = ReplyStack.Take(1);
                            byte[] repl = repl_.ToArray().First();
                            Write(state, repl);

                            ReplyStack.Remove(repl);
                        }

                        flag = false;

                        break;
                    }

                case 0x07e5:
                    {
                        Console.WriteLine("User exit lobby");
                        byte[] packets = new byte[]
                        {
                            0x08, 00, 0xe6, 0x07, 0x00, 0x00, 0x00, 0x00
                        };
                        Write(state, packets);
                        break;
                    }

                case 0x0bbd:
                    {
                        Console.WriteLine("User exit room");
                        byte[] packets = new byte[]
                        {
                            0x08, 0x00, 0xbe, 0x0b, 0x00, 0x00, 0x00, 0x00
                        };

                        if (flag) flag = false;
                        Write(state, packets);
                        break;
                    }

                case 0x0faa:
                    {
                        Console.WriteLine("Song play 1");
                        byte[] packets = new byte[]
                        {
                            0x0c, 0x00, 0xab, 0x0f, 0x00, 0x00, 0x00, 0x00,
                            0x093, 0x21, 0x74, 0x025
                        };
                        Write(state, packets);

                        break;
                    }

                case 0x0fac:
                    {
                        Console.WriteLine("Song play 2");
                        byte[] packets = new byte[]
                        {
                            0x05, 0x00, 0xad, 0x0f, 0x00
                        };
                        Write(state, packets);

                        break;
                    }

                case 0x0fb5:
                    {
                        Console.WriteLine("Song finish/quit?");
                        byte[] packets = new byte[]
                        {
                            0x09, 0x00, 0xb6, 0x0f, 0x00, 0x64, 0x00, 0x00, 0x00
                        };
                        ReplyStack.Clear();
                        Write(state, packets);

                        flag = true;

                        break;
                    }

                case 0x0fb0:
                    {
                        Console.WriteLine("Song finish, Showing result screen");
                        byte[] packets = new byte[]
                        {
                            0x06, 0x00, 0xb1, 0x0f, 0x00, 0x01
                        };
                        Write(state, packets);

                        break;
                    }

                case 0xb10f:
                    {
                        Console.WriteLine("Song finish!");
                        byte[] packets = new byte[]
                        {
                            0x4c, 0x00, 0xb2, 0x0f, 0x08, 0x00, 0x00, 0x00, 0x00,
                            0x01, 0x00, 0x00, 0x00, 0xd7, 0x01, 0x21, 0x00, 0x00,
                            0x00, 0x02, 0x00, 0x21, 0x01, 0x13, 0x00, 0x4f, 0xd2, 
                            0x01, 0x00, 0x00, 0x00, 0x1e, 0x00, 0x00, 0x00, 0xb2, 
                            0xf6, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
                            0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00,
                            0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x05, 0x00, 
                            0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x00, 0x07, 
                            0x00, 0x00, 0x00, 0x00
                        };

                        Write(state, packets);
                        break;
                    }

                case 0x0fae:
                    {
                        Console.WriteLine("Song status check");
                        ws.ReadAgain(state);
                        break;
                    }

                case 0x1771:
                    {
                        Console.WriteLine("TCP Echo");
                        byte[] packets = new byte[length];
                        Buffer.BlockCopy(state.Buffer, 0, packets, 0, length);

                        Write(state, packets);
                        break;
                    }


                default:
                    {
                        Console.WriteLine("Unknown packets {0}, Hex: {1}", cmd, BitConverter.ToString(state.Buffer));
                        ws.ReadAgain(state);
                        break;
                    }
            }
        }

        public int Copy(byte[] res, byte[] file, int offset, int resOffset = -1)
        {
            byte[] newFile = file.Skip(offset).ToArray();

            if (resOffset == -1)
            {
                Buffer.BlockCopy(newFile, 0, res, 0, newFile.Length);
            }
            else
            {
                Buffer.BlockCopy(newFile, 0, res, resOffset, newFile.Length);
            }

            return newFile.Length;
        }

        public void Write(SocketState state, byte[] payload)
        {
            ushort length = payload[0];
            Buffer.BlockCopy(payload, 0, _data, 0, payload[0]);
            DoWrite(state, length);
        }

        public void DoWrite(SocketState state, int length)
        {
            byte[] DataToSend = new byte[length];
            Buffer.BlockCopy(_data, 0, DataToSend, 0, length);

            ws.SendData(state, DataToSend);
        }
    }
}
