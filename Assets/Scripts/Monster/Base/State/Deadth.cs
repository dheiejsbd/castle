using UnityEditor;
using UnityEngine;
using FrameWork.FSM;

public class Death : IState
{
    public Death(GameObject gameObject, Entity entity, AudioClip deathSound)
    {
        this.gameObject = gameObject;
        this.entity = entity;
        this.deathSoundClip = deathSound;
    }

    public StateID Id => StateID.Deadth;

    GameObject gameObject;
    Entity entity;
    AudioClip deathSoundClip;
    public void Enter()
    {
        entity.animator.Play("Death");
        Coroutine.instance.Timer(0.5f, () => entity.RelasePool());
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
