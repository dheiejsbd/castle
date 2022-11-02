using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Message;

public class WaveManager
{
    MonsterManager monsterManager;
    public readonly Message WaveStart = new Message();
    public readonly Message WaveEnd = new Message();
    struct Wave
    {
        public MonsterID ID;
        public float Delay;
    }

    void CreateWave(int Level)
    {

    }

    IEnumerator StartWave(Wave[] waves)
    {
        foreach (var item in waves)
        {
            monsterManager.Spawn(item.ID);
            yield return new WaitForSeconds(item.Delay);
        }

    }
}

