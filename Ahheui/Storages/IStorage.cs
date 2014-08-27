using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui.Storages
{
    public interface IStorage
    {
        void Push(long item);
        void Push(long[] item);
        long Pop();
        void Clear();
        long Peek();
        long[] Copy();
    }
}
