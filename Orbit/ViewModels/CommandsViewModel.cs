using Caliburn.Micro;

namespace Orbit.ViewModels
{
    public class CommandsViewModel: PropertyChangedBase
    {
        private string _command;
        private string _timestamp;
        private string _result;

        /// <summary>
        /// Команда - вкл или выкл
        /// </summary>
        public string Command
        {
            get
            {
                return _command;
            }
            set
            {
                if (value == _command)
                    return;

                _command = value;
                NotifyOfPropertyChange(() => Command);
            }
        }

        /// <summary>
        /// метка времени
        /// </summary>
        public string Timestamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                if (value == _timestamp)
                    return;

                _timestamp = value;
                NotifyOfPropertyChange(() => Timestamp);
            }
        }

        /// <summary>
        /// результат команды
        /// </summary>
        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                if (value == _result)
                    return;

                _result = value;
                NotifyOfPropertyChange(() => Result);
            }
        }
    }
}
