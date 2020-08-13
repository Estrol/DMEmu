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

        public delegate void OnTCPData(object sender, SocketState state);
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
                state.rawBuffer = new byte[SocketState.BUFFER_SIZE];
                state.Data = new MemoryStream();

                state.Socket.BeginReceive(state.rawBuffer, 0, SocketState.BUFFER_SIZE, SocketFlags.None, SocketDataCallback, state);
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
            state.Socket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SocketSendCallback), state);
        }

        private void SocketSendCallback(IAsyncResult result)
        {
            SocketState state = (SocketState)result.AsyncState;
            try
            {
                int byteSend = state.Socket.EndSend(result);
                state.Socket.BeginReceive(state.rawBuffer, 0, SocketState.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(SocketDataCallback), state);
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
            SocketState state = (SocketState)result.AsyncState;
            try
            {
                byte[] tmpBuffer = new byte[state.rawBuffer[0]];
                Buffer.BlockCopy(state.rawBuffer, 0, tmpBuffer, 0, state.rawBuffer[0]);
                state.Buffer = tmpBuffer;
                state.packetLength = tmpBuffer[0];

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
            state.Socket.BeginReceive(state.rawBuffer, 0, SocketState.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(SocketDataCallback), state);
            return true;
        }

        private void Emit(SocketState state)
        {
            if (OnUpdateStatus == null) return;
            OnUpdateStatus(this, state);
        }

        private void CloseSocket(SocketState ci)
        {
            ci.Socket.Close();
        }
    }
    public class SocketState
    {
        public const int BUFFER_SIZE = 10248;
        public Socket Socket;
        public byte[] rawBuffer;
        public byte[] Buffer;
        public MemoryStream Data;
        public int packetLength;
    }
}
