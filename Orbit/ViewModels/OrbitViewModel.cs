using Caliburn.Micro;
using Orbit.Data;
using Orbit.Services;
using System;
using System.Globalization;
using System.Linq;

namespace Orbit.ViewModels
{
    public class OrbitViewModel: PropertyChangedBase
    {
        private string _totalWorkTime;

        /// <summary>
        /// Общее время работы аггрегата
        /// </summary>
        public string TotalWorkTime
        {
            get
            {
                return _totalWorkTime;
            }
            set
            {
                _totalWorkTime = $"Общее время работы насоса в секундах: {value}";
                NotifyOfPropertyChange(() => TotalWorkTime);
            }
        }

        /// <summary>
        /// Список комманд оператора
        /// </summary>
        public BindableCollection<CommandsViewModel> Commands { get; }

        /// <summary>
        /// Выбран ли файл с командами
        /// </summary>
        public bool CanLoadCommands
        {
            get { return _statesFileName != String.Empty; }
        }

        /// <summary>
        /// Сервис для получения данных
        /// </summary>
        private readonly IPumpService _pumpService;

        /// <summary>
        /// Пути к файлам данных
        /// </summary>
        private string _commandsFileName, _statesFileName;

        /// <summary>
        /// ctor
        /// </summary>
        public OrbitViewModel(IPumpService pumpService)
        {
            _pumpService      = pumpService;
            _commandsFileName = "";
            _statesFileName   = "";

            TotalWorkTime = "";
            Commands = new BindableCollection<CommandsViewModel>();
        }

        public void LoadStates()
        {
            _statesFileName   = String.Empty;
            _commandsFileName = String.Empty;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter     = "Xml documents (.xml)|*.xml"
            };

            if (dlg.ShowDialog() == true)
            {
                _statesFileName = dlg.FileName;
            }

            NotifyOfPropertyChange(() => CanLoadCommands);
        }

        public void LoadCommands()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "Xml documents (.xml)|*.xml"
            };

            if (dlg.ShowDialog() == true)
            {
                _commandsFileName = dlg.FileName;
            }

            Update(_pumpService.LoadCommands(_commandsFileName), _pumpService.LoadStates(_statesFileName));
        }

        private void Update(PumpCommands cmds, PumpStates states)
        {
            Commands.Clear();
            TotalWorkTime = String.Empty;

            TimeSpan workingTime = new TimeSpan();

            foreach (Command cmd in cmds.Commands)
            {
                State cmdState = states.States.FirstOrDefault(state => state.Timestamp >= cmd.Timestamp && state.Timestamp <= cmd.Timestamp + new TimeSpan(0, 0, 0, 5));

                string result = String.Empty;

                if (cmdState != null)
                {
                    if (cmdState.IsOn)                    result = "Насос включился";
                    else
                    {
                        if (!cmdState.PressureAvail)      result = "Насос не включился: низкое давление на входе";
                        else if (!cmdState.VoltageAvail)  result = "Насос не включился: отсутствует напряжение";
                        else                              result = "Насос отключился";
                    }
                }
                else
                {
                    result = "Насос не отреагировал на команду";
                }

                Commands.Add(new CommandsViewModel
                {
                    Command   = cmd.Value,
                    Timestamp = cmd.Timestamp.ToString(CultureInfo.InvariantCulture),
                    Result    = result,
                });
            }

            for (int i = 1; i < states.States.Count; i++)
            {
                if (!states.States[i].IsOn && states.States[i - 1].IsOn)
                    workingTime += states.States[i].Timestamp - states.States[i - 1].Timestamp;
            }

            TotalWorkTime = $"{workingTime.Days} дней {workingTime.Hours} часов {workingTime.Minutes} минут {workingTime.Seconds} секунд";
        }
    }
}
