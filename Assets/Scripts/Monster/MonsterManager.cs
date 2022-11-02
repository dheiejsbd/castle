using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterManager : MonoBehaviour
{
    private List<IObjectPool<Entity>> pool = new List<IObjectPool<Entity>>();
    [SerializeField] GameObject[] MonsterPrefabs;
    [SerializeField] Path[] paths;
    enum PathID
    {
        Line,
        Sine,
    }
    List<Entity> EnableMonster = new List<Entity>();

    Entity attackSuccessMonster;
    PlayerController player;
    private void Awake()
    {
        AddPool(MonsterID.Skelaton,
               (IObjectPool<Entity> pool, GameObject obj) => new Skelaton(paths[(int)PathID.Line],
                pool, obj));

        AddPool(MonsterID.Shilder,
               (IObjectPool<Entity> pool, GameObject obj) => new Shilder(paths[(int)PathID.Line],
                pool, obj));


        attackSuccessMonster = null;
        player = GameObject.FindObjectOfType<PlayerController>();
    }
    void AddPool(MonsterID ID, Func<IObjectPool<Entity>, GameObject, Entity> NewEntity, int maxSize = 20)
    {
        pool.Add(new ObjectPool<Entity>(() => CreateMonster((int)ID, (IObjectPool<Entity> pool) =>
                                                                {
                                                                    return NewEntity(pool, Instantiate( MonsterPrefabs[(int)ID]));
                                                                }),
                                                                GetMonster,
                                                                ReleaseMonster,
                                                                DestroyMonster,
                                                                maxSize: maxSize));
    }
    void AttackSuccess(Entity entity)
    {
        Time.timeScale = 0.3f;
        attackSuccessMonster = entity;
        player.HitMessage.Send(entity);
    }
    private void Update()
    {
        if(attackSuccessMonster != null)
        {
            return;
        }


        foreach (var item in EnableMonster)
        {
            item.Update();
        }
    }

    public void Spawn(MonsterID ID)
    {
        var obj = pool[(int)ID].Get();
        obj.Initialize();
    }

    #region Pooling Method
    Entity CreateMonster(int i, Func<IObjectPool<Entity>, Entity> func)
    {
        var entity = func(pool[i]);
        entity.AttackSuccess += AttackSuccess;
        return entity;
    }
    void GetMonster(Entity monster)
    {
        EnableMonster.Add(monster);
        monster.gameObject.SetActive(true);
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
            if (item.death) continue;
            if (item.gameObject.transform.position.x < 0 != left) continue;
            if (near == null) near = item;
            if (Mathf.Abs(near.gameObject.transform.position.x) > Mathf.Abs(item.gameObject.transform.position.x))
            {
                near = item;
            }
        }
        return near;
    }
}
