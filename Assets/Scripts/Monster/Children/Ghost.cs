using FrameWork.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

class Ghost : PathMonster
{
    public Ghost(Path path, IObjectPool<Entity> pool, EntityData data) : base(path, pool, data)
    {
        MaxHealth = 1;
        FSM.Add(new MoveByPath(gameObject, this));
        FSM.Add(new Attack(gameObject, this));
        FSM.Add(new Death(gameObject, this, data.deathSound));

        EnterEventPointEvent += (Entity e) => FSM.Switch(StateID.Deadth);
        EnterEventPointEvent += (Entity e) => hitAble = false;
        DeathEvent += (Entity e) => FSM.Switch(StateID.Attack);
        DeathEvent += (Entity e) => Debug.Log("Attack");
        EnterPathPercent = 0.05f;
    }
    bool hitAble = true;
    public override bool death => !hitAble;


    public override void Initialize()
    {
        hitAble = true;
        base.Initialize();
        FSM.Switch(StateID.MoveByPath);
    }

    public override void Update()
    {
        FSM.Update();
    }
}
