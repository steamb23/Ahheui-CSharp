﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamB23.Ahheui.Storages
{
    public class Stack : IStorage
    {
        Stack<long> stack;
        public Stack()
        {
            stack = new Stack<long>();
        }
        public Stack(IEnumerable<long> collection)
        {
            stack = new Stack<long>(collection);
        }
        public Stack(int capacity)
        {
            stack = new Stack<long>(capacity);
        }

        void IStorage.Push(long item)
        {
            stack.Push(item);
        }

        long IStorage.Pop()
        {
            try
            {
                return stack.Pop();
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }
        long IStorage.Peek()
        {
            try
            {
                return stack.Peek();
            }
            catch(InvalidOperationException)
            {
                return 0;
            }
        }
        void IStorage.Clear()
        {
            stack.Clear();
        }
    }
}
