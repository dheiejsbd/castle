using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    class Node
    {
        public Action enter;
        public Action hit;
    }
    [SerializeField] float nodeSpeed;
    [SerializeField] float nodeDist = 1;
    float currentNodeDist;
    Node[] nodes = new Node[6];
    int currentNodeID;


    public void OnStart()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new Node();
        }
    }

    public void OnUpdate()
    {
        currentNodeDist += Time.deltaTime * nodeSpeed;
        
        if(currentNodeDist >= 1)
        {
            currentNodeID++;
            if(currentNodeID == nodes.Length) currentNodeID = 0;
            currentNodeDist -= 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int i = 0; i < nodes.Length; i++)
        {
            float x = GetNodeXPos(i);
            Gizmos.DrawRay(new Vector3(x, 0), Vector3.up);
        }
    }

    float GetNodeXPos(int nodeID)
    {
        int order = nodeID + currentNodeID;
        if (order >= nodes.Length) order -= nodes.Length;
        order++;
        return order * nodeDist - currentNodeDist * nodeDist;
    }
}
