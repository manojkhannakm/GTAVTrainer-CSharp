using System.Collections.Generic;
using System.IO;
using GTAVTrainer.Event;
using GTAVTrainer.Net.Client;
using GTAVTrainer.Net.Server;

namespace GTAVTrainer
{
    public class Script : GTA.Script
    {
        private readonly Server _server;

        private Client _client;

        public Script()
        {
            if (!File.Exists(Path.ChangeExtension(Filename, ".ini")))
            {
                Settings.SetValue(Constants.Settings, Constants.Port, Constants.DefaultPort);
                Settings.SetValue(Constants.Settings, Constants.Password, Constants.DefaultPassword);
                Settings.Save();
            }

            var port = Settings.GetValue(Constants.Settings, Constants.Port, Constants.DefaultPort);
            if (port < 1024 || port > 65535)
            {
                port = Constants.DefaultPort;
            }

            var password = Settings.GetValue(Constants.Settings, Constants.Password, Constants.DefaultPassword);

            _server = new Server(port, password);
            _server.ClientConnected += client =>
            {
                if (_client != null)
                {
                    _client.Disconnect();
                }

                _client = client;
            };
            _server.Connect();

            var dictionary = new Dictionary<string, EventHandler>();
            dictionary.Add("client", new ClientHandler(this));
            dictionary.Add("map", new MapHandler(this));

            Tick += (sender, args) =>
            {
                if (_client != null)
                {
                    var readEventData = _client.Read();
                    if (readEventData != null)
                    {
                        var writeEventData = dictionary[readEventData.HandlerName].Handle(readEventData);
                        if (writeEventData != null)
                        {
                            _client.Write(writeEventData);
                        }

                        if (readEventData.HandlerName == "client" && readEventData.Name == "disconnected")
                        {
                            _client.Disconnect();
                            _client = null;
                        }
                    }
                }
            };
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _server.Disconnect();

                if (_client != null)
                {
                    _client.Disconnect();
                }
            }
        }
    }
}