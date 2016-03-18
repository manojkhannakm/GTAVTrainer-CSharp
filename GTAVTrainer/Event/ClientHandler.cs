using GTA;
using GTAVTrainer.Net.Data;

namespace GTAVTrainer.Event
{
    public class ClientHandler : EventHandler
    {
        public ClientHandler(Script script) : base(script)
        {
            Dictionary.Add("connected", Connected);
            Dictionary.Add("disconnected", Disconnected);
        }

        public EventData Connected(EventData readEventData)
        {
            UI.Notify("~h~GTA V Trainer~n~" +
                      "~h~~g~Client Connected!");

            return null;
        }

        public EventData Disconnected(EventData readEventData)
        {
            UI.Notify("~h~GTA V Trainer~n~" +
                      "~h~~r~Client Disconnected!");

            return null;
        }
    }
}