using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Message;

public class PlayerInput
{
    public readonly Message LeftAttack = new Message();
    public readonly Message RightAttack = new Message();

    public void Update()
    {
        if(Input.touchCount != 0)
        {
            foreach (var item in Input.touches)
            {
                if(item.phase == TouchPhase.Began)
                {
                    if (item.position.x < Screen.width / 2)
                        LeftAttack.Send();
                    else
                        RightAttack.Send();
                    return;
                }
            }
        }
    }
}
