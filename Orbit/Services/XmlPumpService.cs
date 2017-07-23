using Orbit.Data;
using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Orbit.Services
{
    public class XmlPumpService : IPumpService
    {
        public PumpCommands LoadCommands(string path) => ReadXml<PumpCommands>(path);
        public PumpStates   LoadStates(string path) => ReadXml<PumpStates>(path);

        private T ReadXml<T>(string path) where T : class, new()
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException($"Файл {path} не найден");

                using (StreamReader reader = new StreamReader(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return (T) serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show($"Выбраны неверные файлы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new T();
            }
        }
    }
}
