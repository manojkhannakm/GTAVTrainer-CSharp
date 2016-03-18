using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace GTAVTrainer.Net.Client
{
    public class Reader
    {
        private readonly StreamReader _streamReader;
        private readonly Queue<string> _queue;
        private readonly Thread _thread;

        public Reader(NetworkStream networkStream)
        {
            _streamReader = new StreamReader(networkStream);

            _queue = new Queue<string>();

            _thread = new Thread(() =>
            {
                var line = _streamReader.ReadLine();
                if (line != null)
                {
                    lock (_queue)
                    {
                        _queue.Enqueue(line);
                    }
                }
            });
            _thread.Start();
        }

        public string ReadLine()
        {
            lock (_queue)
            {
                if (_queue.Count > 0)
                {
                    return _queue.Dequeue();
                }
            }

            return null;
        }

        public void Close()
        {
            _streamReader.Close();
            _thread.Stop();
        }
    }
}