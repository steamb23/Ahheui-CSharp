using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{
    public class BackupEventArgs : EventArgs
    {
        internal BackupEventArgs(Backup backup)
        {
            this.backup = backup;
        }
        public Backup backup;
    }
}
