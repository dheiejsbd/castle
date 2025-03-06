using UnityEditor;
using UnityEngine;
using FrameWork.FSM;
using DG.Tweening;

public class Attack : IState
{
    public Attack(GameObject gameObject, Entity entity)
    {
        this.gameObject = gameObject;
        this.entity = entity;
    }

    public StateID Id => StateID.Attack;

    GameObject gameObject;
    Entity entity;

    public void Enter()
    {
        Debug.Log("Enter");
        if (entity.death) return;
        entity.animator.Play("Attack");
        entity.dismiss = true;
        entity.renderer.DOFade(0, 0.5f).SetDelay(0.3f);
        Coroutine.instance.Timer(0.8f, () => entity.RelasePool());
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        entity.renderer.DOFade(1, 0);
    }
}
