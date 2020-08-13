using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DMEmu.Data;

namespace DMEmu
{
    public class Session
    {
        public SocketServer ws;
        public Form1 Main;
        public short port;

        public static int MAX_PAYLOAD = 20249;
        public static bool flag = false;

        public Dictionary<string, byte[]> SptFiles = new Dictionary<string, byte[]>();
        public List<byte[]> ReplyStack = new List<byte[]>();
        public byte[] _data = new byte[MAX_PAYLOAD];

        public bool IsOJNListAvailable = false;
        public OJNList MusicList;

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

        public OJNList GetList()
        {
            if (this.IsOJNListAvailable) {
                return this.MusicList;
            }
            else throw new Exception("OJNList not loaded.");
        }

        public void LoadData() 
        {
            SptFiles.Clear();

            Console.WriteLine("Loading Channel1.spt");
            byte[] Channel1 = File.ReadAllBytes(Application.StartupPath + @"\Spt\Channel1.Spt");
            SptFiles.Add("Channel1", Channel1);

            if (Directory.Exists(Application.StartupPath + @"\Image")) {
                Console.WriteLine("Loading OJNList.dat");
                if (File.Exists(Application.StartupPath + @"\Image\OJNList.dat")) {
                    this.MusicList = OJNListDecoder.Decode(Application.StartupPath + @"\Image\OJNList.dat");
                    this.IsOJNListAvailable = true;
                }
            } else {
                Console.WriteLine("Loading MusicList.spt");
                byte[] MusicList = File.ReadAllBytes(Application.StartupPath + @"\Spt\MusicList.Spt");
                SptFiles.Add("MusicList", MusicList);
            }

            Console.WriteLine("Loading D007.spt");
            byte[] D007 = File.ReadAllBytes(Application.StartupPath + @"\Spt\D007.Spt");
            SptFiles.Add("D007", D007);

            Console.WriteLine("Loading D207.spt");
            byte[] D207 = File.ReadAllBytes(Application.StartupPath + @"\Spt\D207.Spt");
            SptFiles.Add("D207", D207);
        }

        private void onState(object o, SocketState state) {
            this.ZeroMemory();

            ushort packetLength = BitConverter.ToUInt16(state.Buffer, 0);
            ushort packetID = BitConverter.ToUInt16(state.Buffer, 2);

            switch (packetID)
            {
                case 0x03f1:
                    {
                        Console.WriteLine("null >> STATE_LOGIN");
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
                        Console.WriteLine("Socket disconnected!");
                        ws.Close(state);
                        break;
                    }

                case 0x03ec:
                    {

                        byte[] packets = new byte[]
                        {
                            0x10, 0x00, 0xed, 0x03, 0x00, 0x00, 0x00, 0x00,
                            0x00, 0x00, 0x00, 0x00, 0x87, 0x53, 0x00, 0x00
                        };

                        Write(state, packets);
                        break;
                    }

                case 0x03ea:
                    {
                        Console.WriteLine("STATE_LOGIN >> STATE_PLANET");
                        int count = Copy(_data, SptFiles["Channel1"], 0);
                        DoWrite(state, count);
                        break;
                    }

                case 0x0fbe:
                    {
                        if (this.IsOJNListAvailable) { 
                            Console.WriteLine("[Experimental] Construct OJNList packet");
                            var headers = MusicList.GetHeaders();
                            using var mstream = new MemoryStream();
                            using (var writer = new BinaryWriter(mstream)) {
                                short PacketLength = (short)(6 + (headers.Length * 12) + 12);
                                writer.Write(PacketLength); // Total packet length
                                writer.Write(new byte[] { 0xBF, 0x0F }); // Header?
                                writer.Write((short)MusicList.Count);

                                foreach (OJN ojn in headers) {
                                    writer.Write((short)ojn.Id);
                                    writer.Write((short)ojn.NoteCountEx);
                                    writer.Write((short)ojn.NoteCountNx);
                                    writer.Write((short)ojn.NoteCountHx);
                                    writer.Write(new byte[4]);
                                }

                                writer.Write(new byte[12]);

                                byte[] data = mstream.ToArray();
                                Console.WriteLine("Length {0}", data.Length);
                                Console.WriteLine("Length2 {0}", BitConverter.ToInt16(data, 0));
                                int count = Copy(_data, data, 0);
                                DoWrite(state, count);
                            }
                        } else {
                            int count = Copy(_data, SptFiles["MusicList"], 6);
                            DoWrite(state, count);
                        }
                        break;
                    }

                case 0x07d0:
                    {
                        int count = Copy(_data, SptFiles["D007"], 20);
                        DoWrite(state, count);
                        break;
                    }

                case 0x07d2:
                    {
                        Console.WriteLine("STATE_PLANET >> STATE_LOBBY");
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
                        ws.ReadAgain(state);
                        break;
                    }

                case 0x13a4:
                    {
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

                        Console.WriteLine("STATE_LOBBY >> STATE_WAITING, Room creation: {0}", Encoding.UTF8.GetString(roomName));
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
                        byte[] packets = new byte[]
                        {
                            0x04, 0x00, 0xe9, 0x07
                        };
                        Write(state, packets);
                        break;
                    }

                case 0x03e8:
                    {
                        byte[] packets = new byte[]
                        {
                            0x08, 0x00, 0xe9, 0x03, 0x00, 0x00, 0x00, 0x00
                        };

                        Write(state, packets);
                        break;
                    }

                case 0x03ef:
                    {
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
                        state.Buffer[2] = 0xa1;

                        if (!flag)
                        {
                            _data = state.Buffer;
                            DoWrite(state, _data[0]);
                        }
                        else
                        {
                            byte[] cp = new byte[packetLength];
                            Buffer.BlockCopy(state.Buffer, 0, cp, 0, packetLength);
                            ReplyStack.Add(cp);
                            ws.ReadAgain(state);
                        }
                        break;
                    }

                case 0x0fa4:
                    {
                        byte[] packets = new byte[] { 0x06, 0x00, 0xa5, 0x0f, 0x00, 0x00 };
                        ReplyStack.Add(packets);
                        ws.ReadAgain(state);

                        break;
                    }

                case 0x0fb7:
                    {
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
                        Console.WriteLine("STATE_LOBBY >> STATE_LOGIN/STATE_PLANET");
                        byte[] packets = new byte[]
                        {
                            0x08, 00, 0xe6, 0x07, 0x00, 0x00, 0x00, 0x00
                        };
                        Write(state, packets);
                        break;
                    }

                case 0x0bbd:
                    {
                        Console.WriteLine("STATE_WAITING >> STATE_LOBBY");
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
                        Console.WriteLine("STATE_WAITING >> STATE_PLAYING");
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
                            0x09, 0x00, 0xb6, 0x0f, 0x00, 0x64, 0x00, 0x00, 0x00 // packets[6] => Character level
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

                        byte[] packets2 = new byte[]
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
                        Write(state, packets2);

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
                        byte[] packets = new byte[packetLength];
                        Buffer.BlockCopy(state.Buffer, 0, packets, 0, packetLength);

                        Write(state, packets);
                        break;
                    }


                default:
                    {
                        Console.WriteLine("Unknown packets {0}, Hex: {1}", packetID, BitConverter.ToString(state.Buffer));
                        ws.ReadAgain(state);
                        break;
                    }
            }
        }

        public void ZeroMemory()
        {
            this._data = new byte[MAX_PAYLOAD];
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

    public enum Packets
    {
        Login = 0x3f1,
        Login2 = 0x03f3,
        Disconnect = 0xfff0,

    }
}
