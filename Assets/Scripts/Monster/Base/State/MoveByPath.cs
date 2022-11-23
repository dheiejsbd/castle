using UnityEditor;
using UnityEngine;
using FrameWork.FSM;

public class MoveByPath : IState
{
    public MoveByPath(GameObject gameObject, Entity entity)
    {
        this.gameObject = gameObject;
        this.entity = entity;
    }

    public StateID Id => StateID.MoveByPath;

    GameObject gameObject;
    Entity entity;
    float moveSpeed = 1;
    public void Enter()
    {
        entity.animator.Play("Move");
    }

    public void Execute()
    {
        var path = entity as PathMonster;
        path.Move(moveSpeed * Time.deltaTime);
    }

    public void Exit()
    {
    }
}
