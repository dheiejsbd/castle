using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public enum Type
    {
        sine,
        line,
        size
    }
    public static Path[] paths;

    [SerializeField] Type type;
    [SerializeField] float startPos;
    [SerializeField] float hight;
    [SerializeField] int quality;
    [SerializeField, Range(0, 1)] List< float> point;

    private void Awake()
    {
        if(paths == null)
        {
            paths = new Path[(int)Type.size];
        }
        paths[(int)type] = this;
    }
    public static Path GetPath(Type type)
    {
        return paths[(int)type];
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (type == Type.sine)
        {
            Vector3 lastPos = Vector3.zero;
            for (int i = 1; i <= quality; i++)
            {
                var a = getSinePosition(i / (float)quality, false);
                Gizmos.DrawLine(lastPos, a);
                lastPos = a;
            }
            Gizmos.color = Color.red;
            for (int i = 0; i < point.Count; i++)
            {
                Gizmos.DrawSphere(getSinePosition(point[i], false), 0.1f);
            }
        }
        else
        {
            Gizmos.DrawLine(Vector3.zero, getLinePosition(1, false));
            Gizmos.color = Color.red;

            for (int i = 0; i < point.Count; i++)
            {
                Gizmos.DrawSphere(getLinePosition(point[i], false), 0.1f);
            }
        }
    }

    public Vector3 GetPosition(float x, bool flip)
    {
        if(type == Type.sine)
        {
            return getSinePosition(x, flip);
        }
        else
        {
            return getLinePosition(x, flip);
        }
    }

    Vector3 getSinePosition(float x, bool flip)
    {
        var a = new Vector3(x * startPos * (flip? -1:1), Mathf.Sin(x* Mathf.PI) * hight);
        return a;
    }
    Vector3 getLinePosition(float x, bool flip)
    {
        return new Vector3(x * startPos* (flip? -1:1), 0);
    }
}
