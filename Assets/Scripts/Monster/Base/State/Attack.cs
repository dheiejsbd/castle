using UnityEditor;
using UnityEngine;
using FrameWork.FSM;

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
        entity.animator.Play("Attack");
    }

    public void Execute()
    {

    }

    public void Exit()
    {
    }
}
