using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

class Skelaton : PathMonster
{
    public Skelaton(Path path, IObjectPool<Entity> pool, GameObject gameObject) : base(path, pool, gameObject)
    {
        animator = gameObject.GetComponent<Animator>();
        HitEvent += (Entity entity)=> Death();
        EnterEventPoint += AttackAnim;
        DeathEvent += DeathAnim;
        DeathEvent += (Entity entity) => Coroutine.instance.StartCor(RelasePool(1));
    }
    Animator animator;

    void AttackAnim(Entity entity)
    {
        animator.Play("Attack");
    }
    void DeathAnim(Entity entity)
    {
        animator.Play("Death");
    }
}
