using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using FrameWork.Message;
using System;
using Random = UnityEngine.Random;
using static UnityEditor.Progress;
using DG.Tweening;

public class WaveManager : MonoBehaviour
{
    MonsterManager monsterManager;
    public event Action WaveEnd;
    [SerializeField] GameObject ExclamationMarkPrefab;
    [SerializeField] Transform canvas;
    [SerializeField] float ExclamtionOffset;
    [SerializeField] float beat = 1;
    [SerializeField] float ExclamtionGabTime = 0.1f;
    [SerializeField] float ExclamtionShowTime = 0.2f;
    ObjectPooling ExclamationPool;

    IEnumerator waveCor;
    struct Wave
    {
        public Wave (MonsterID id, float delay)
        {
            ID = id;
            Delay = delay;
            flip = Random.Range(0, 2) == 0;
        }
        public MonsterID ID;
        public float Delay;
        public bool flip;
    }


    public void OnStart()
    {
        ExclamationPool = new ObjectPooling(ExclamationMarkPrefab, canvas);
        monsterManager = GetComponent<MonsterManager>();
    }

    public void StartWave(int Level)
    {
        waveCor = StartWave(CreateWave(Level));
        Coroutine.instance.StartCor(waveCor);
    }

    public void StopWave()
    {
        Coroutine.instance.StopCoroutine(waveCor);
    }

    Wave[] CreateWave(int Level)
    {
        List<Wave> waves = new List<Wave>();
        for (int i = 0; i < 10; i++)
        {
            bool ran = Random.Range(0, 100) <= 30;
            waves.Add(new Wave(ran? MonsterID.Shilder : MonsterID.Goblin, beat));
        }
        return waves.ToArray();
    }

    IEnumerator StartWave(Wave[] waves)
    {
        foreach (var item in waves)
        {
            Q(item);
            
            yield return new WaitForSeconds(item.Delay / beat * ExclamtionGabTime);

        }
        
        yield return new WaitForSeconds(1);

        foreach (var item in waves)
        {
            monsterManager.Spawn(item.ID, item.flip);
            yield return new WaitForSeconds(item.Delay);
        }

        WaveEnd?.Invoke();
    }

    void Q(Wave wave)
    {
        var obj = ExclamationPool.Get();
        monsterManager.GetYPos(wave.ID);

        var rectTr = obj.GetComponent<RectTransform>();
        if(wave.flip)
        {
            rectTr.anchorMin = Vector2.zero;
            rectTr.anchorMax = Vector2.zero;
            rectTr.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            rectTr.anchorMin = new Vector2(1,0);
            rectTr.anchorMax = new Vector2(1,0);
            rectTr.eulerAngles = Vector3.zero;
        }

        rectTr.anchoredPosition = new Vector2(0,Camera.main.WorldToScreenPoint(new Vector3(0, monsterManager.GetYPos(wave.ID), 0)).y + Random.Range(0,ExclamtionOffset));
        Image img = obj.GetComponent<Image>();

        Color imgCorlor = img.color;
        imgCorlor.a = 0;
        img.color = imgCorlor;

        img.DOFade(1, ExclamtionShowTime).SetEase(Ease.OutExpo).SetLoops(2, LoopType.Yoyo).OnComplete(()=>ExclamationPool.Release(obj));
    }
}

