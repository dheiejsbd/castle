using FrameWork.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Canvas canvas;
    [SerializeField] List<UIWidget> widgets = new List<UIWidget>();
    
    public void OnStart()
    {
        instance = this;
        GameObject.DontDestroyOnLoad(canvas);
        foreach (var item in widgets)
        {
            item.Deactivate();
        }
    }
    public UIWidget GetWindow(Type widgetType)
    {
        foreach (var item in widgets)
        {
            if(item.GetType() == widgetType)
            {
                return item;
            }
        }
        throw new NullReferenceException($"{widgetType} Not Found");
    }
}
