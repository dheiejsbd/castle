using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    public static Coroutine instance;
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    public void StartCor(IEnumerator enumerator)
    {
        StartCoroutine(enumerator);
    }
}
