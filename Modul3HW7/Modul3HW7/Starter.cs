using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modul3HW7
{
    internal class Starter
    {
        private Logger _logger = new Logger();
        private int n = 0;
        public Starter()
        {
            _logger.AskBackup += StoreAsync;
            n = int.Parse(File.ReadAllText("settings.txt"));
        }

        public async Task StoreAsync(List<string> logg, int endIndex)
        {
            var directory = "Backup";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fileExtention = ".txt";
            var fileName = directory + "\\" + DateTime.Now.ToString("hh.mm.ss.ffff dd.MM.yyyy") + fileExtention;
            await File.WriteAllLinesAsync(fileName, logg.Take(endIndex));
        }

        public async Task Run()
        {
            var temp = this.LoggingAsynс();
            var temp2 = this.LoggingAsynс();
            await Task.WhenAll(temp, temp2);
        }

        public async Task LoggingAsynс()
        {
            for (int i = 0; i < 50; i++)
            {
                    await _logger.AddRec(n);
            }
        }
    }
}
