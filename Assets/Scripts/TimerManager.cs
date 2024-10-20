using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public List<SimpleTimer> timers = new List<SimpleTimer>();

    private float currentTime = 0;

    public float RefreshTime = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update all timers once per second
        currentTime += Time.deltaTime;
        if (currentTime > RefreshTime)
        {
            currentTime = currentTime % RefreshTime;

            List<SimpleTimer> tempList = new List<SimpleTimer>(timers);

            foreach (SimpleTimer timer in tempList)
            {
                timer.Update();
            }
        }
    }

    public void AddTimer(SimpleTimer timer)
    {
        timers.Add(timer);
    }

    public void RemoveTimer(SimpleTimer timer)
    {
        timers.Remove(timer);
    }
}

public class SimpleTimer
{
    private GameObject owner;
    bool repeat = false;

    Action callback;

    float currentTime = 0;
    float duration = 0;

    public SimpleTimer(Action callback, GameObject owner, float duration, bool repeat)
    {
        this.callback = callback;
        this.owner = owner;
        this.repeat = repeat;
        this.duration = duration;
        currentTime = duration;
    }

    public void Update()
    {
        currentTime -= TimerManager.Instance.RefreshTime;

        if (currentTime <= 0)
        {
            if (owner == null)
            {
                TimerManager.Instance.RemoveTimer(this);
                return;
            }

            callback.Invoke();

            if (repeat)
            {
                currentTime = duration;
            }
            else
            {
                TimerManager.Instance.RemoveTimer(this);
            }
        }
    }

}