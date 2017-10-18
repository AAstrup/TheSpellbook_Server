using System;
using System.Collections.Generic;

public class Counter : IUpdatable
{
    public float duration;
    private List<timesOut> triggersWhenTimesOut;
    public delegate void timesOut();

    public Counter(float duration, List<timesOut> triggersWhenTimesOut)
    {
        this.duration = duration;
        this.triggersWhenTimesOut = triggersWhenTimesOut;
    }

    public bool HasExpired()
    {
        return duration < 0;
    }

    public void Update(float deltaTime)
    {
        duration -= deltaTime;
    }

    public void End()
    {
        foreach (var item in triggersWhenTimesOut)
        {
            item.Invoke();
        }

    }
}