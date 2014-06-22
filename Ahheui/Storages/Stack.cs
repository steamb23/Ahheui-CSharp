using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui.Storages
{
    public class Stack : IStorage
    {
        Stack<double> stack;
        public Stack()
        {
            stack = new Stack<double>();
        }
        public Stack(IEnumerable<double> collection)
        {
            stack = new Stack<double>(collection);
        }
        public Stack(int capacity)
        {
            stack = new Stack<double>(capacity);
        }

        void IStorage.Push(double item)
        {
            stack.Push(item);
        }

        double IStorage.Pop()
        {
            return stack.Pop();
        }
        double IStorage.Peek()
        {
            return stack.Peek();
        }
        void IStorage.Clear()
        {
            stack.Clear();
        }
    }
}
