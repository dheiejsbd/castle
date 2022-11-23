using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    public static Coroutine instance;
    public void OnStart()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    public void StartCor(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }

    public void Timer(float Time, Action action)
    {
        StartCor(TimerCor(Time, action));
    }
    IEnumerator TimerCor (float Time, Action action)
    {
        yield return new WaitForSeconds(Time);
        action?.Invoke();
    }


    public void TimerAtRealTime(float Time, Action action)
    {
        StartCor(TimerCorAtRealTime(Time, action));
    }
    IEnumerator TimerCorAtRealTime (float Time, Action action)
    {
        yield return new WaitForSecondsRealtime(Time);
        action?.Invoke();
    }
}
