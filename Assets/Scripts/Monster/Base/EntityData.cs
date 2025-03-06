using UnityEditor;
using UnityEngine;

public abstract class EntityData : ScriptableObject
{
    public abstract MonsterID id { get; }

    public GameObject gameObject;
    public int progressValue;

    public AudioClip deathSound;
    public AudioClip attackSound;
    public AudioClip hitSound;

    public abstract Entity Instance(UnityEngine.Pool.IObjectPool<Entity> pool);
}

public abstract class PathEntityData: EntityData
{
    public Path.Type pathtype;
}