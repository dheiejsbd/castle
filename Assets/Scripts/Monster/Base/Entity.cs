using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using FrameWork.FSM;
using Object = UnityEngine.Object;

public abstract class Entity
{
    public Entity(IObjectPool<Entity> pool, EntityData data)
    {
        this.data = data;
        managedPool = pool;

        gameObject = Object.Instantiate(data.gameObject);

        void GetComponents()
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
            animator = gameObject.GetComponent<Animator>();
            animationEvent = gameObject.GetComponent<EntityAnimationEvent>();
        }
        GetComponents();

        animationEvent.AttackMessage.AddListener(() => AttackSuccessEvent?.Invoke(this));
    }








    private IObjectPool<Entity> managedPool;
    public StateMachine FSM = new StateMachine();
    public EntityData data { get; }

    public int Health = 1;
    public int MaxHealth = 1;

    public GameObject gameObject;
    public Animator animator;

    protected SpriteRenderer renderer;
    protected EntityAnimationEvent animationEvent;


    public virtual bool death { get { return Health <= 0; } }

    protected bool flip;
    public bool dismiss { get { return death || _dismiss; } set { _dismiss = value; } }
    private bool _dismiss;
    
    /// <summary>
    /// AnimationKeyFrameEvent
    /// </summary>
    public event Action<Entity> AttackSuccessEvent;
    /// <summary>
    /// Player공격시 이벤트 발생
    /// </summary>
    public event Action<Entity> HitEvent;
    /// <summary>
    /// Entity.Death()
    /// </summary>
    public event Action<Entity> DeathEvent;

    public virtual void Initialize()
    {
        Health = MaxHealth;
    }
    public abstract void Update();

    public void SetFlip(bool flip)
    {
        this.flip = flip;
    }
    public virtual float GetYPos()
    {
        return 0;
    }



    public virtual void Hit()
    {
        Health--;
        if(Health <= 0)
        {
            Death();
            return;
        }
        HitEvent?.Invoke(this);
    }
    public virtual void Death()
    {
        DeathEvent?.Invoke(this);
    }

    public void RelasePool(float Time = 0.5f)
    {
        Coroutine.instance.Timer(Time, () => managedPool.Release(this));
    }
}
