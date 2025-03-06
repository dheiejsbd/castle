using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using FrameWork.Page;
using FrameWork.Loading;
using Player;
using FrameWork.Message;

public class InGamePage : Page
{
    public override int ID => (int)PageID.Ingame;


    PlayerController player;
    public MonsterManager monsterManager { get; private set; }
    CameraController cameraController;
    WaveManager waveManager;
    NodeManager nodemanager;

    int level = 0;
    Value<int> progress = new Value<int>(0);

    InGameWindow window;
    int TargetPrograss;


    public InGamePage(PageManager pageManager) : base(pageManager)
    {
        window = UIManager.instance.GetWindow(typeof(InGameWindow)) as InGameWindow;
        progress.SetFilter((int amount) => { return Mathf.Clamp(amount, 0, TargetPrograss); });
        progress.AddListner((int progress) => { window.SetPrograss((float)progress / TargetPrograss); });
        progress.AddListner((int progress) => { if(progress == TargetPrograss) LevelClear(); });

    }

    public override void Initialize()
    {
        TargetPrograss = 10;
        progress.Set(0);
        window.SetHp(1);
    }

    public override IEnumerator Prepare()
    {
        yield return LoadingProcess.instance.LoadScene("Ingame");
    }
    public override void Enter()
    {
        window.Activate();

        void GetComponents()
        {
            cameraController = GameObject.FindObjectOfType<CameraController>();
            player = GameObject.FindObjectOfType<Player.PlayerController>();
            monsterManager = GameObject.FindObjectOfType<MonsterManager>();
            waveManager = GameObject.FindObjectOfType<WaveManager>();
            nodemanager = GameObject.FindObjectOfType<NodeManager>();
        }
        GetComponents();

        void InitializeComponent()
        {
            monsterManager.OnAwake();

            cameraController.OnStart();
            player.OnStart();
            waveManager.OnStart();
            nodemanager.OnStart();
        }
        InitializeComponent();

        void SetPlayer()
        {
            player.health.AddListner((int hp) => window.SetHp((float)hp / player.State.Health));

            player.AttackSuccess += (Entity en) => Coroutine.instance.Timer(0.1f, () => cameraController.Shake(1f, 0.125f / 2, 2));//tnwjd
            player.AttackSuccess += (Entity en) => Time.timeScale = 0;
            player.AttackSuccess += (Entity en) => Coroutine.instance.TimerAtRealTime(0.05f, () => Time.timeScale = 1);

            player.DeathMessage.AddListener((Entity en) => waveManager.StopWave());
            player.DeathMessage.AddListener((Entity en) => cameraController.ZoomIn());
            player.GetNearByPlayerMonster = (bool left) => monsterManager.GetNearByPlayerMonster(left);
        }
        SetPlayer();

        monsterManager.EntityDeathEvent += (Entity entity) => progress.Set(progress.Get + entity.data.progressValue);
    }

    public override void Update()
    {
        monsterManager.OnUpdate();
        player.OnUpdate();
    }

    public override IEnumerator Exit()
    {
        monsterManager = null;
        cameraController = null;
        waveManager = null;
        player = null;
        yield return LoadingProcess.instance.UnloadScene("Ingame");
        window.Deactivate();
    }


    void LevelClear()
    {
        waveManager.StopWave();
        Debug.Log("Level Clear");
    }
}