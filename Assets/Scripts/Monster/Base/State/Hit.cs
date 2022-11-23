using UnityEditor;
using UnityEngine;
using FrameWork.FSM;
using System.Collections;
using System;

public class Hit : IState
{
    public Hit(GameObject gameObject, Entity entity)
    {
        this.gameObject = gameObject;
        this.entity = entity;
    }

    public StateID Id => StateID.Hit;

    GameObject gameObject;
    Entity entity;
    float kuckBackSpeed = 2;
    public void Enter()
    {
        SoundManager.instance.PlayEffect(entity.data.hitSound);
        entity.animator.Play("Hit");
        entity.dismiss = true;
        var moveByPath = entity as PathMonster;
        moveByPath.Move(-0.1f);
        Coroutine.instance.Timer(0.125f, () => entity.FSM.Switch(StateID.MoveByPath));
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        entity.dismiss = false;
    }
}
