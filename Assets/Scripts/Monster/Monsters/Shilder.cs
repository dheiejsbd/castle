using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

class Shilder : PathMonster
{
    bool shild = true;
    public Shilder(Path path, IObjectPool<Entity> pool, GameObject gameObject) : base(path, pool, gameObject)
    {
        animator = gameObject.GetComponent<Animator>();
        HitEvent += (Entity entity) => Hit();
        EnterEventPoint += AttackAnim;
        DeathEvent += DeathAnim;
        DeathEvent += (Entity entity) => Coroutine.instance.StartCor(RelasePool(1));
    }
    Animator animator;
    public override void Initialize()
    {
        base.Initialize();
        shild = true;
    }
    void AttackAnim(Entity entity)
    {
        animator.Play("Attack");
    }

    public override void Hit()
    {
        if(shild)
        {
            Move(-0.1f);
            shild = false;
        }
        else
        {
            Death();
        }
    }
    void DeathAnim(Entity entity)
    {
        animator.Play("Death");
    }

}
