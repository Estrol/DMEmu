using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace DMEmu
{
    public class SocketServer
    {
        private static List<SocketState> _clientSockets = new List<SocketState>();
        private static Socket _serverSocket;
        private short _port;

        public delegate void OnTCPData(object sender, TCPData e);
        public event OnTCPData OnUpdateStatus;

        public SocketServer(short port)
        {
            _port = port;
        }

        public void Start()
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
            _serverSocket.Listen(_port);
            Console.WriteLine("Socket now listening at port {0}", _port);
            _serverSocket.BeginAccept(new AsyncCallback(SocketConnectionCallback), _serverSocket);
        }

        private void SocketConnectionCallback(IAsyncResult result)
        {
            Console.WriteLine("A client connected");
            SocketState state = new SocketState();
            try
            {
                Socket newSocket = (Socket)result.AsyncState;
                state.Socket = newSocket.EndAccept(result);
                state.Buffer = new byte[SocketState.BUFFER_SIZE];
                state.Data = new MemoryStream();

                state.Socket.BeginReceive(state.Buffer, 0, SocketState.BUFFER_SIZE, SocketFlags.None, SocketDataCallback, state);
                _serverSocket.BeginAccept(new AsyncCallback(SocketConnectionCallback), _serverSocket);
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException)
                {
                    Console.WriteLine("A client disconnected");
                    CloseSocket(state);
                }
                else if (e is SocketException)
                {
                    SocketException err = (SocketException)e;
                    if (err.ErrorCode == 10054)
                    {
                        Console.WriteLine("A client disconnected");
                        CloseSocket(state);
                    }
                }

                Console.WriteLine(e.Message);
            }
        }

        public void SendData(SocketState state, byte[] data)
        {
            Console.WriteLine("[ DEBUG ] Sending data....");
            foreach (byte a in data) Console.Write(" {0}", Convert.ToString(a, 16));
            Console.WriteLine();
            state.Socket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SocketSendCallback), state);
        }

        private void SocketSendCallback(IAsyncResult result)
        {
            SocketState state = (SocketState)result.AsyncState;
            try
            {
                int byteSend = state.Socket.EndSend(result);
                Console.WriteLine("[ DEBUG ] Sending {0} bytes", byteSend);
                Console.WriteLine("[ DEBUG ] Sending complete, Now waiting another packets");
                state.Socket.BeginReceive(state.Buffer, 0, SocketState.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(SocketDataCallback), state);
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException)
                {
                    Console.WriteLine("A client disconnected");
                    CloseSocket(state);
                }
                else if (e is SocketException)
                {
                    SocketException err = (SocketException)e;
                    if (err.ErrorCode == 10054)
                    {
                        Console.WriteLine("A client disconnected");
                        CloseSocket(state);
                    }
                }

                Console.WriteLine(e.Message);
            }
        }

        private void SocketDataCallback(IAsyncResult result)
        {
            Console.WriteLine("[ DEBUG ] Success receiving data, now emitting...");
            SocketState state = (SocketState)result.AsyncState;
            try
            {
                state.readBytes = state.Socket.EndReceive(result);
                Emit(state);
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException)
                {
                    Console.WriteLine("A client disconnected");
                    CloseSocket(state);
                }
                else if (e is SocketException)
                {
                    SocketException err = (SocketException)e;
                    if (err.ErrorCode == 10054)
                    {
                        Console.WriteLine("A client disconnected");
                        CloseSocket(state);
                    }
                }

                Console.WriteLine(e.Message);
            }
        }

        public bool Close(SocketState state)
        {
            CloseSocket(state);
            return true;
        }

        public bool ReadAgain(SocketState state)
        {
            state.Socket.BeginReceive(state.Buffer, 0, SocketState.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(SocketDataCallback), state);
            return true;
        }

        private void Emit(SocketState state)
        {
            if (OnUpdateStatus == null) return;

            TCPData data = new TCPData(state.Buffer, state);
            OnUpdateStatus(this, data);
        }

        private void CloseSocket(SocketState ci)
        {
            ci.Socket.Close();
        }
    }
    public class SocketState
    {
        public const int BUFFER_SIZE = 225;
        public Socket Socket;
        public byte[] Buffer;
        public MemoryStream Data;
        public int readBytes;
    }

    public class TCPData : EventArgs
    {
        public byte[] Buffer { get; private set; }
        public SocketState State;

        public TCPData(byte[] data, SocketState stateRaw)
        {
            Buffer = data;
            State = stateRaw;
        }
    }
}
