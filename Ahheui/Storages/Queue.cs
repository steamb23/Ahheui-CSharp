using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui.Storages
{
    public class Queue : IStorage
    {
        Queue<double> queue;
        public Queue()
        {
            queue = new Queue<double>();
        }
        public Queue(IEnumerable<double> collection)
        {
            queue = new Queue<double>(collection);
        }
        public Queue(int capacity)
        {
            queue = new Queue<double>(capacity);
        }

        void IStorage.Push(double item)
        {
            queue.Enqueue(item);
        }

        double IStorage.Pop()
        {
            return queue.Dequeue();
        }
        double IStorage.Peek()
        {
            return queue.Peek();
        }
        void IStorage.Clear()
        {
            queue.Clear();
        }
    }
}
