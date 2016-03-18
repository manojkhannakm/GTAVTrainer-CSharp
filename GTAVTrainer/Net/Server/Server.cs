using System.IO;
using System.Net;
using System.Net.Sockets;

namespace GTAVTrainer.Net.Server
{
    public class Server
    {
        public delegate void ClientConnectedHandler(Client.Client client);

        private readonly TcpListener _tcpListener;
        private readonly Thread _thread;

        public Server(int port, string password)
        {
            _tcpListener = new TcpListener(IPAddress.Any, port);

            _thread = new Thread(() =>
            {
                _tcpListener.Start();

                var tcpClient = _tcpListener.AcceptTcpClient();

                var streamReader = new StreamReader(tcpClient.GetStream());
                var passwordMatched = streamReader.ReadLine() == password;

                var streamWriter = new StreamWriter(tcpClient.GetStream());
                streamWriter.WriteLine(passwordMatched.ToString().ToLower());
                streamWriter.Flush();

                if (passwordMatched)
                {
                    if (ClientConnected != null)
                    {
                        ClientConnected(new Client.Client(tcpClient));
                    }
                }
                else
                {
                    tcpClient.Close();
                    streamReader.Close();
                    streamWriter.Close();
                }
            });
        }

        public event ClientConnectedHandler ClientConnected;

        public void Connect()
        {
            _thread.Start();
        }

        public void Disconnect()
        {
            _tcpListener.Stop();
            _thread.Stop();
        }
    }
}