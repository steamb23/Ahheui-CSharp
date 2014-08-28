using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{
    public class Backup
    {
        internal Backup(Storages.IStorage[] storages)
        {
            this.storages = storages;
        }
        internal Storages.IStorage[] storages;
    }
}
