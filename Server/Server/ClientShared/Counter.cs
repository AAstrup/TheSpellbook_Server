using System;
using System.Collections.Generic;

public class Counter : IUpdatableDeltaTime
{
    private float totalDuration;
    public float duration;
    private List<timesOut> triggersWhenTimesOut;
    public delegate void timesOut();
    public event timeUpdated timeUpdateEvent;
    public delegate void timeUpdated(float currentTime,float totalTime);


    public Counter(float duration, List<timesOut> triggersWhenTimesOut)
    {
        totalDuration = duration;
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
        if(timeUpdateEvent != null)
            timeUpdateEvent.Invoke(duration,totalDuration);
    }

    public void End()
    {
        foreach (var item in triggersWhenTimesOut)
        {
            item.Invoke();
        }

    }
}