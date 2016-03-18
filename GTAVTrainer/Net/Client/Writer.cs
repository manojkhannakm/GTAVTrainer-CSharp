using System.IO;
using System.Net.Sockets;

namespace GTAVTrainer.Net.Client
{
    public class Writer
    {
        private readonly StreamWriter _streamWriter;

        public Writer(NetworkStream networkStream)
        {
            _streamWriter = new StreamWriter(networkStream);
        }

        public void WriteLine(string line)
        {
            _streamWriter.WriteLine(line);
            _streamWriter.Flush();
        }

        public void Close()
        {
            _streamWriter.Close();
        }
    }
}