using System;
using System.Threading;

namespace GTAVTrainer
{
    public class Thread
    {
        private readonly System.Threading.Thread _thread;

        private volatile bool _running;

        public Thread(ThreadStart threadStart)
        {
            _thread = new System.Threading.Thread(() =>
            {
                while (_running)
                {
                    try
                    {
                        threadStart.Invoke();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            });
        }

        public void Start()
        {
            _running = true;
            _thread.Start();
        }

        public void Stop()
        {
            _running = false;
            _thread.Join();
        }
    }
}