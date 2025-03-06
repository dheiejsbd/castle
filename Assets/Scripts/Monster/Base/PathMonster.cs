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
    public PathMonster(Path path, IObjectPool<Entity> pool, EntityData data) : base(pool, data)
    {
        this.path = path;
        CollisionEnter += (HitBox hitbox, Collision2D collision) => AttackSuccess();
    }

    Path path;
    public float pathPercent { get; private set; }
    public override void Initialize()
    {
        base.Initialize(); 
        pathPercent = 1;
        renderer.flipX = !flip;
        gameObject.transform.position = path.GetPosition(pathPercent, flip);
    }
    public void Move(float dist)
    {
        pathPercent -= dist;
        pathPercent = Mathf.Clamp01(pathPercent);
        gameObject.transform.position = path.GetPosition(pathPercent, flip);
    }

    public override float GetYPos()
    {
        return path.GetPosition(0.5f,false).y;
    }
}
