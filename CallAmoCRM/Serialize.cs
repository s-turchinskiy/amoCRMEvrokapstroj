using System;
using System.IO;
using System.Xml.Serialization;

namespace AmoDownloaderCLR_TestApp
{
    public static class StaticSerializer
    {
        public static bool Save(Type static_class, string fileName)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Settings));

                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, Settings.Instance);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Load(Type static_class, string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    StaticSerializer.Save(static_class, fileName);
                }
                XmlSerializer formatter = new XmlSerializer(typeof(Settings));
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    Settings settings = (Settings)formatter.Deserialize(fs);
                    settings.SetSettings(settings);

                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при загрузке настроек: " + ex.Message);
                return false;
            }
        }
    }


}
