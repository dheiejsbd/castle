using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Goblin", menuName = "ScriptableObject/EntityData/Goblin", order = int.MaxValue)]
public class GoblinData : PathEntityData
{
    public override MonsterID id => MonsterID.Goblin;

    public override Entity Instance(UnityEngine.Pool.IObjectPool<Entity> pool)
    {
        return new Normal(Path.GetPath(pathtype), pool, this);
    }
}
