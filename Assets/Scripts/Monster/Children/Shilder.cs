using FrameWork.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

class Shilder : PathMonster
{
    public Shilder(Path path, IObjectPool<Entity> pool, EntityData data) : base(path, pool, data)
    {
        MaxHealth = 2;
        FSM.Add(new MoveByPath(gameObject, this));
        FSM.Add(new Attack(gameObject, this));
        FSM.Add(new Hit(gameObject, this));
        FSM.Add(new Death(gameObject, this, data.deathSound));

        HitEvent += (Entity e) => FSM.Switch(StateID.Hit);
        EnterEventPointEvent += (Entity e) => FSM.Switch(StateID.Attack);
        DeathEvent += (Entity e) => FSM.Switch(StateID.Deadth);
    }
    GameObject Shild;


    public override void Initialize()
    {
        base.Initialize();
        FSM.Switch(StateID.MoveByPath);
    }

    public override void Update()
    {
        FSM.Update();
    }

}
