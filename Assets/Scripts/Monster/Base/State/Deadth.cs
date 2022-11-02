﻿using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Monster.Base.State
{
    public class Deadth : ScriptableObject
    {
        [MenuItem("Tools/MyTool/Do It in C#")]
        static void DoIt()
        {
            EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        }
    }
}