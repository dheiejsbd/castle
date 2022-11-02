using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Entity
{
    public Entity(IObjectPool<Entity> pool, GameObject gameObject)
    {
        this.gameObject = gameObject;
        managedPool = pool;
        renderer = gameObject.GetComponent<SpriteRenderer>();
        animationEvent = gameObject.GetComponent<EntityAnimationEvent>();
        animationEvent.AttackMessage.AddListener(() => AttackSuccess(this));
    }

    private IObjectPool<Entity> managedPool;
    public GameObject gameObject;
    protected SpriteRenderer renderer;
    protected EntityAnimationEvent animationEvent;
    public bool death { get; protected set; }

    public event Action<Entity> EnterEventPoint;
    public event Action<Entity> AttackSuccess;
    public event Action<Entity> HitEvent;
    public event Action<Entity> DeathEvent;

    public abstract void Initialize();
    public abstract void Update();
    public virtual void Hit()
    {
        HitEvent?.Invoke(this);
    }
    public virtual void Death()
    {
        death = true;
        DeathEvent?.Invoke(this);
    }

    protected IEnumerator RelasePool(float Time = 0)
    {
        yield return new WaitForSeconds(Time);
        managedPool.Release(this);
    }
    protected void Enter()
    {
        EnterEventPoint?.Invoke(this);
    }
}
