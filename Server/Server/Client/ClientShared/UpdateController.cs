using System;
using System.Collections.Generic;

public class UpdateController
{
    private List<IUpdatableDeltaTime> updatables;

    public UpdateController()
    {
        updatables = new List<IUpdatableDeltaTime>();
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

    public void Add(IUpdatableDeltaTime updatable)
    {
        updatables.Add(updatable);
    }
}