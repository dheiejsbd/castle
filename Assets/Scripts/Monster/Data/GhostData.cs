using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostData", menuName = "ScriptableObject/EntityData/Ghost", order = int.MaxValue)]
public class GhostData : PathEntityData
{
    public override MonsterID id => MonsterID.Ghost;

    public override Entity Instance(UnityEngine.Pool.IObjectPool<Entity> pool)
    {
        return new Ghost(Path.GetPath(pathtype), pool, this);
    }
}
