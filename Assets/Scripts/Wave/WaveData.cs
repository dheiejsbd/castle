using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObject/wave")]
public class WaveData : ScriptableObject
{
    public MonsterID[] waves;
}
