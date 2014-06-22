using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui.Storages
{
    class Queue : Queue<double>, IStorage
    {
        public Queue()
            : base()
        {
        }
        public Queue(IEnumerable<double> collection)
            : base(collection)
        {
        }
        public Queue(int capacity)
            : base(capacity)
        {
        }

        void IStorage.Push(double item)
        {
            base.Enqueue(item);
        }

        double IStorage.Pop()
        {
            return base.Dequeue();
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
