using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using FrameWork.Page;
using Player;

public class InGame : IPage
{
    public int ID => (int)PageID.Ingame;


    PlayerController player;
    public MonsterManager monsterManager { get; private set; }
    CameraController cameraController;
    WaveManager waveManager;


    public void Initialize()
    {
    }

    public void Enter()
    {
        void GetComponents()
        {
            cameraController = GameObject.FindObjectOfType<CameraController>();
            player = GameObject.FindObjectOfType<Player.PlayerController>();
            monsterManager = GameObject.FindObjectOfType<MonsterManager>();
            waveManager = GameObject.FindObjectOfType<WaveManager>();
        }
        GetComponents();

        void InitializeComponent()
        {
            monsterManager.OnAwake();

            cameraController.OnStart();
            player.OnStart();
            waveManager.OnStart();
        }
        InitializeComponent();

        void SetPlayer()
        {
            player.AttackSuccess += (Entity en) => Coroutine.instance.Timer(0.1f, () => cameraController.Shake(1f, 0.125f / 2, 2));//tnwjd
            player.AttackSuccess += (Entity en) => Time.timeScale = 0;
            player.AttackSuccess += (Entity en) => Coroutine.instance.TimerAtRealTime(0.05f, () => Time.timeScale = 1);
            player.HitMessage.AddListener((Entity en) => waveManager.StopWave());
            player.HitMessage.AddListener((Entity en) => cameraController.ZoomIn());
            player.GetNearByPlayerMonster = (bool left) => monsterManager.GetNearByPlayerMonster(left);
        }
        SetPlayer();

        void SetWaveManager()
        {
            waveManager.StartWave(0);
            waveManager.WaveEnd += () => waveManager.StartWave(0);
        }
        SetWaveManager();
    }

    public void Update()
    {
        monsterManager.OnUpdate();
        player.OnUpdate();
    }

    public void Exit()
    {
        monsterManager = null;
        cameraController = null;
        waveManager = null;
        player = null;
    }
}