using System;
using System.Collections.Generic;

public class UpdateController
{
    private List<IUpdatable> updatables;

    public UpdateController()
    {
        updatables = new List<IUpdatable>();
    }

    public void Update(float deltaTime)
    {
        for (int i = 0; i < updatables.Count; i++)
        {
            updatables[i].Update(deltaTime);
            if (updatables[i].HasExpired())
            {
                updatables[i].End();
                updatables.RemoveAt(i);
            }
        }
    }

    public void Add(IUpdatable updatable)
    {
        updatables.Add(updatable);
    }
}