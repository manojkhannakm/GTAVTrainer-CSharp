using System.Collections.Generic;
using GTAVTrainer.Net.Data;

namespace GTAVTrainer.Event
{
    public class EventHandler
    {
        protected readonly Script Script;
        protected readonly Dictionary<string, Event> Dictionary;

        protected EventHandler(Script script)
        {
            Script = script;
            Dictionary = new Dictionary<string, Event>();
        }

        public EventData Handle(EventData readEventData)
        {
            return Dictionary[readEventData.Name].Invoke(readEventData);
        }

        protected delegate EventData Event(EventData eventData);
    }
}