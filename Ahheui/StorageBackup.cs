using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui
{
    public struct StorageBackup
    {
        internal StorageBackup(Storages.IStorage[] storages)
        {
            this.storages = storages;
        }
        internal Storages.IStorage[] storages;
    }
}
