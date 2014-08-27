using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui.Storages
{
    public class Queue : IStorage
    {
        Queue<long> queue;
        public Queue()
        {
            queue = new Queue<long>();
        }
        public Queue(IEnumerable<long> collection)
        {
            queue = new Queue<long>(collection);
        }
        public Queue(int capacity)
        {
            queue = new Queue<long>(capacity);
        }
        void IStorage.Push(long item)
        {
            queue.Enqueue(item);
        }
        void IStorage.Push(long[] item)
        {
            foreach (var temp in item)
            {
                queue.Enqueue(temp);
            }
        }
        long IStorage.Pop()
        {
            return queue.Dequeue();
        }
        long IStorage.Peek()
        {
            try
            {
                return queue.Peek();
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }
        void IStorage.Clear()
        {
            queue.Clear();
        }
        long[] IStorage.Copy()
        {
            List<long> result = new List<long>();
            foreach (var temp in queue)
            {
                result.Add(temp);
            }
            return result.ToArray();
            
        }
    }
}
