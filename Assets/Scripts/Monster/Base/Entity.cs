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
            hitBoxes = gameObject.GetComponents<HitBox>();
            foreach (HitBox hitBox in hitBoxes)
            {
                hitBox.Enter +=(HitBox hitbox, Collision2D collision) => CollisionEnter?.Invoke(hitbox, collision);
            }
        }
        GetComponents();
    }

    public event Action<HitBox, Collision2D> CollisionEnter;






    private IObjectPool<Entity> managedPool;
    public StateMachine FSM = new StateMachine();
    public EntityData data { get; }

    public int Health = 1;
    public int MaxHealth = 1;

    public GameObject gameObject;
    public Animator animator;
    public Rigidbody2D rd;
    public HitBox[] hitBoxes;

    public SpriteRenderer renderer;
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
        dismiss= false;
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


    public void AttackSuccess()
    {
        AttackSuccessEvent?.Invoke(this);
    }
    public virtual void Hit()
    {
        if(dismiss) return;
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

    public void RelasePool()
    {
        managedPool.Release(this);
    }
}
