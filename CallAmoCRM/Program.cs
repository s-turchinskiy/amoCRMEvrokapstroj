using AmoCRM;
using AmoDownloaderCLR_TestApp;
using System;

namespace CallAmoCRM
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!StaticSerializer.Load(typeof(Settings), "Settings.xml"))
            {
                return;
            }

            var work = new Work();
            work.GetFromAmoCRMSendTo1C(Settings.Instance.HostAmoCRM, Settings.Instance.ClientId,
                 Settings.Instance.ClientSecret,
                 Settings.Instance.Host1cDebugGet,
                 Settings.Instance.Host1cDebugPost);

            Console.WriteLine("все");
            Console.ReadKey();
        }
    }
}
