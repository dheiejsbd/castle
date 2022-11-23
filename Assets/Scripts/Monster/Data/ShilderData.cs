using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ShilderData", menuName = "ScriptableObject/EntityData/Shilder", order = int.MaxValue)]
public class ShilderData : PathEntityData
{
    public override MonsterID id => MonsterID.Shilder;

    public override Entity Instance(UnityEngine.Pool.IObjectPool<Entity> pool)
    {
        return new Shilder(Path.GetPath(pathtype), pool, this);
    }
}
