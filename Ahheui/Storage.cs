﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Storages = SteamB23.Ahheui.Storages;

namespace SteamB23.Ahheui
{
    class Storage
    {
        Storages.IStorage[] storages;
        public Storage()
        {
            storages = new Storages.IStorage[28];
            // 0~25 = Stack
            for (int i = 0; i < (int)Syntax.Index.ㅍ; i++)
            {
                storages[i] = new Storages.Stack();
            }
            // 26 = Queue
            storages[26] = new Storages.Queue();
            // 27 = Stack
            // ㅎ받침의 정확한 쓰임새가 연구되지 않았으므로 스택으로 선언.
            storages[27] = new Storages.Stack();

        }
        public Storages.IStorage this[int index]
        {
            get
            {
                return storages[index];
            }
        }
        public Storages.IStorage this[Syntax.Index index]
        {
            get
            {
                return storages[(int)index];
            }
        }
        public int Length
        {
            get
            {
                return storages.Length;
            }
        }
        public void Clear()
        {
            foreach (var temp in storages)
            {
                temp.Clear();
            }
        }
    }
}
