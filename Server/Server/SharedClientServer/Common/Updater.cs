using System;
using System.Collections.Generic;

public class Updater
{
    List<IUpdatable> updateList;
    public Updater()
    {
        updateList = new List<IUpdatable>();
    }
    public void Add(IUpdatable updatable)
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

    public void Remove(IUpdatable updatable)
    {
        updateList.Remove(updatable);
    }
}