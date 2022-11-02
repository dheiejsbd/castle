using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    Player.PlayerController player;
    public MonsterManager monsterManager { get; private set; }
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = GameObject.FindObjectOfType<Player.PlayerController>();
        monsterManager = GameObject.FindObjectOfType<MonsterManager>();
        StartCoroutine(s());
    }
    void Update()
    {
        
    }
    IEnumerator s()
    {
        while (true)
        {

        yield return new WaitForSeconds(1f);
        monsterManager.Spawn(MonsterID.Shilder);
        }
    }
}
