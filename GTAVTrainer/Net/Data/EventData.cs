using System.Text.RegularExpressions;

namespace GTAVTrainer.Net.Data
{
    public class EventData : Data
    {
        public EventData(string handlerName, string name)
        {
            HandlerName = handlerName;
            Name = name;
        }

        public EventData(string data) : base(data.Substring(data.IndexOf("(")))
        {
            var match = Regex.Match(data, "([a-z_0-9]+)\\.([a-z_0-9]+)\\(.*\\)");
            HandlerName = match.Groups[1].Value;
            Name = match.Groups[2].Value;
        }

        public string HandlerName { get; }

        public string Name { get; }

        public override string ToString()
        {
            return HandlerName + "." + Name + base.ToString();
        }
    }
}