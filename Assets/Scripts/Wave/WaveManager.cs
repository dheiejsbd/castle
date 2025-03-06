using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using FrameWork.Message;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;

public class WaveManager : MonoBehaviour
{
    MonsterManager monsterManager;
    public event Action WaveEnd;

    IEnumerator waveCor;

    [SerializeField] Pattern[] patterns;
    [SerializeField] WaveData[] monsters;


    struct Wave
    {
        public Wave (MonsterID id, bool flip)
        {
            ID = id;
            this.flip = flip;
        }
        public MonsterID ID;
        public bool flip;
    }


    public void OnStart()
    {
        monsterManager = GetComponent<MonsterManager>();
    }

    Wave[] CreateWave(int Level)
    {
        return CreateWave(monsters[0], patterns[0]);
    }

    Wave[] CreateWave(WaveData data, Pattern pattern)
    {
        Wave[] waves = new Wave[data.waves.Length];
        bool flip = Random.Range(0, 2) == 0;
        for (int i = 0; i < data.waves.Length; i++)
        {
            bool f = pattern.left[i];
            waves[i] = new Wave(data.waves[i], flip? f: !f);
        }
        return waves;
    }

}

