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
    }

    /// <summary>
    /// Move 도중 자동으로 이벤트 발생
    /// </summary>
    public event Action<Entity> EnterEventPointEvent;

    Path path;
    float pathPercent;
    protected float EnterPathPercent = 0.1f;
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
        pathPercent = Mathf.Clamp(pathPercent, EnterPathPercent, 1);
        if (pathPercent <= EnterPathPercent)
        {
            EnterEventPointEvent?.Invoke(this);
            return;
        }
        gameObject.transform.position = path.GetPosition(pathPercent, flip);
    }

    public override float GetYPos()
    {
        return path.GetPosition(0.5f,false).y;
    }
}
