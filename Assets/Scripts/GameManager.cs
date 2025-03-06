using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Page;
using FrameWork.Loading;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public PageManager pageManager;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        pageManager = new PageManager();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GetComponent<Coroutine>().OnStart();
        GetComponent<UIManager>().OnStart();
        GetComponent<LoadingProcess>().OnStart();
        pageManager.Start();
    }


    void Update()
    {
        pageManager.Update();
    }

}
