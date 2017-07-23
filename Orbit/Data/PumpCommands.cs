using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Orbit.Data
{
    [XmlRoot("commands")]
    public class PumpCommands
    {
        [XmlElement("command")]
        public List<Command> Commands { get; set; } = new List<Command>();
    }

    [XmlRoot("command")]
    public class Command
    {
        [XmlElement("value")]
        public string Value { get; set; }

        [XmlIgnore]
        public DateTime Timestamp
        {
            get { return DateTime.Parse(_Timestamp);}
        }

        [XmlElement("timestamp")]
        public string _Timestamp { get; set; }
    }
}
