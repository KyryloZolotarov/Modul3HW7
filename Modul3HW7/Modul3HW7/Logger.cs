using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul3HW7
{
    internal class Logger
    {
        private List<string> _loggRecords = new List<string>();
        private int x = 0;

        public delegate Task NeedBackup(List<string> messege, int x);
        public event NeedBackup AskBackup;

        public async Task AddRec(int n)
        {
            _loggRecords.Add($"{DateTime.Now.ToShortTimeString()}: message");
            if (_loggRecords.Count % n == 0)
            {
                x++;
                await AskBackup.Invoke(_loggRecords, n * x);
            }
        }

        public List<string> GetLogs()
        {
            return _loggRecords;
        }
    }
}
