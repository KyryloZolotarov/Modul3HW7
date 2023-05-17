using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul3HW7
{
    internal class Logger
    {
        private readonly SemaphoreSlim semaphoreSlim;
        private List<string> _loggRecords = new List<string>();
        public Logger()
        {
            semaphoreSlim = new SemaphoreSlim(1, 1);
        }

        public delegate Task NeedBackup(List<string> messege);
        public event NeedBackup AskBackup;
        public async Task LoggingAsynk()
        {
            var n = File.ReadAllText("settings.txt");
            var n1 = int.Parse(n);
            for (int i = 0; i < 50; i++)
            {
                await semaphoreSlim.WaitAsync();
                try
                {
                    await AddRec();
                    if (_loggRecords.Count % n1 == 0)
                    {
                        await AskBackup.Invoke(_loggRecords);
                    }

                    await Task.Delay(200);
                }
                finally
                {
                    semaphoreSlim.Release();
                }
            }
        }

        private Task AddRec()
        {
            _loggRecords.Add($"{DateTime.Now.ToShortTimeString()}: message");
            return Task.CompletedTask;
        }
    }
}
