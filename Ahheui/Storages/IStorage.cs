using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui.Storages
{
    public interface IStorage
    {
        void Push(double item);
        double Pop();
        void Clear();
        double Peek();
    }
}
