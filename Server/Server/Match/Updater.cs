using System;
using System.Collections.Generic;

namespace Match
{
    public class Updater
    {
        List<IUpdatable> updateList;
        public Updater()
        {
            updateList = new List<IUpdatable>();
        }
        internal void Add(IUpdatable updatable)
        {
            updateList.Add(updatable);
        }

        public void Update()
        {
            for (int i = 0; i < updateList.Count; i++)
            {
                updateList[i].Update();
            }
        }

        internal void Remove(IUpdatable updatable)
        {
            updateList.Remove(updatable);
        }
    }
}