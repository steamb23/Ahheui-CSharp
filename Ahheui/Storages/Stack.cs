using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui.Storages
{
    public class Stack : Stack<double>, IStorage
    {
        public Stack()
            : base()
        {
        }
        public Stack(IEnumerable<double> collection)
            : base(collection)
        {
        }
        public Stack(int capacity)
            : base(capacity)
        {
        }

        void IStorage.Push(double item)
        {
            base.Push(item);
        }

        double IStorage.Pop()
        {
            return base.Pop();
        }
        double IStorage.Peek()
        {
            return base.Peek();
        }
        void IStorage.Clear()
        {
            base.Clear();
        }
    }
}
