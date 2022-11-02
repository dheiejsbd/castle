using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
abstract class PathMonster : Entity
{
    public PathMonster(Path path, IObjectPool<Entity> pool, GameObject gameObject) : base(pool, gameObject)
    {
        this.path = path;
    }

    bool flip;
    Path path;
    float pathPercent;
    public override void Initialize()
    {
        pathPercent = 1;
        death = false;
        flip = Random.Range(0, 2) == 0;
        renderer.flipX = !flip;
        gameObject.transform.position = path.GetPosition(pathPercent, flip);
    }
    protected void Move(float dist)
    {
        pathPercent -= dist;
        pathPercent = Mathf.Clamp01(pathPercent);
        if (pathPercent <= 0.1f)
        {
            Enter();
            return;
        }
        gameObject.transform.position = path.GetPosition(pathPercent, flip);
    }

    public override void Update()
    {
        if (death) return;
        Move(Time.deltaTime);
    }
}
