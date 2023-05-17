using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul3HW7
{
    internal class Starter
    {
        private Logger _logger = new Logger();
        public Starter()
        {
            _logger.AskBackup += StoreAsync;
        }
        public async Task StoreAsync(List<string> logg)
        {
            var directory = "Backup";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var fileExtention = ".txt";
            var fileName = directory + "\\" + DateTime.Now.ToString("hh.mm.ss dd.MM.yyyy") + fileExtention;
            await File.WriteAllLinesAsync(fileName, logg);
        }
        public async Task Run()
        {
            var temp = _logger.LoggingAsynk();
            var temp2 = _logger.LoggingAsynk();
            await Task.WhenAll(temp, temp2);
        }
    }
}
