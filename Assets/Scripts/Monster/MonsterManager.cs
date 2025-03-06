using DG.Tweening;
using FrameWork.Page;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterManager : MonoBehaviour
{
    private Dictionary<MonsterID, IObjectPool<Entity>> pool = new Dictionary<MonsterID, IObjectPool<Entity>>();
    [SerializeField] SerializableDictionary<MonsterID, EntityData> MonsterDatas;
    [SerializeField] Path[] paths;
    enum PathID
    {
        Line,
        Sine,
    }
    List<Entity> EnableMonster = new List<Entity>();

    PlayerController player;

    public event Action<Entity> EntityDeathEvent;

    public void OnAwake()
    {
        AddPool(MonsterID.Goblin);
        AddPool(MonsterID.Shilder);
        AddPool(MonsterID.Ghost);

        player = GameObject.FindObjectOfType<PlayerController>();
    }
    void AddPool(MonsterID ID, int maxSize = 20)
    {
        var data = MonsterDatas[ID];
        pool.Add(ID, new ObjectPool<Entity>(() => CreateMonster(ID, (IObjectPool<Entity> pool) =>
                                                                {
                                                                    var entity = data.Instance(pool);
                                                                    entity.DeathEvent += (Entity entity) => { EntityDeathEvent?.Invoke(entity); };
                                                                    entity.gameObject.SetActive(false);
                                                                    return entity;
                                                                }),
                                                                GetMonster,
                                                                ReleaseMonster,
                                                                DestroyMonster,
                                                                maxSize: maxSize));
    }
    void AttackSuccess(Entity entity)
    {
        if (entity.death) return;
        player.HitMessage.Send(entity);
    }


    public void OnUpdate()
    {
        foreach (var item in EnableMonster)
        {
            item.Update();
        }
    }
    

    public void Spawn(MonsterID ID, bool flip)
    {
        var obj = pool[ID].Get();
        obj.SetFlip(flip);
        obj.Initialize();
        obj.gameObject.SetActive(true);
    }
    public float GetYPos(MonsterID ID)
    {
        var obj = pool[ID].Get();
        var pos = obj.GetYPos();
        obj.RelasePool();
        return pos;
    }


    #region Pooling Method
    Entity CreateMonster(MonsterID i, Func<IObjectPool<Entity>, Entity> func)
    {
        var entity = func(pool[i]);
        entity.AttackSuccessEvent += AttackSuccess;
        return entity;
    }
    void GetMonster(Entity monster)
    {
        EnableMonster.Add(monster);
        monster.Initialize();
    }
    void ReleaseMonster(Entity monster)
    {
        EnableMonster.Remove(monster);
        monster.gameObject.SetActive(false);
    }
    void DestroyMonster(Entity monster)
    {
        Destroy(monster.gameObject);
    }
    #endregion


    public Entity GetNearByPlayerMonster(bool left)
    {
        Entity near = null;
        foreach (var item in EnableMonster)
        {
            if (item.dismiss) continue;
            if (item.gameObject.transform.position.x < 0 != left) continue;
            if (near == null) near = item;
            if (Mathf.Abs(near.gameObject.transform.position.x) > Mathf.Abs(item.gameObject.transform.position.x))
            {
                near = item;
            }
        }
        return near;
    }
    public void Initialize()
    {
    }
}
