using UnityEditor;
using UnityEngine;
using FrameWork.FSM;
public class Idle : IState
{
    public Idle (GameObject gameObject, Entity entity)
    {
        this.gameObject = gameObject;
        this.entity = entity;
    }

    public StateID Id => StateID.Idle;

    GameObject gameObject;
    Entity entity;

    public void Enter()
    {
        entity.animator.Play("Idle");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
