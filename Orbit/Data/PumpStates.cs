using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Orbit.Data
{
    [XmlRoot("states")]
    public class PumpStates
    {
        [XmlElement("state")]
        public List<State> States { get; set; } = new List<State>();
    }

    [XmlRoot("state")]
    public class State
    {
        [XmlElement("voltageIsAvailable")]
        public bool VoltageAvail { get; set; }

        [XmlElement("pressureIsAvailable")]
        public bool PressureAvail { get; set; }

        [XmlElement("statusIsOn")]
        public bool IsOn { get; set; }

        [XmlIgnore]
        public DateTime Timestamp
        {
            get { return DateTime.Parse(_Timestamp); }
        }

        [XmlElement("timestamp")]
        public string _Timestamp { get; set; }
    }
}
