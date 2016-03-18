using System.Net.Sockets;
using GTAVTrainer.Net.Data;

namespace GTAVTrainer.Net.Client
{
    public class Client
    {
        private readonly TcpClient _tcpClient;
        private readonly Reader _reader;
        private readonly Writer _writer;

        public Client(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _reader = new Reader(tcpClient.GetStream());
            _writer = new Writer(tcpClient.GetStream());
        }

        public EventData Read()
        {
            var line = _reader.ReadLine();
            if (line != null)
            {
                return new EventData(line);
            }

            return null;
        }

        public void Write(EventData eventData)
        {
            _writer.WriteLine(eventData.ToString());
        }

        public void Disconnect()
        {
            _tcpClient.Close();
            _reader.Close();
            _writer.Close();
        }
    }
}