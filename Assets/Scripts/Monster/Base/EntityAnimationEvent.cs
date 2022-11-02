using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using FrameWork.Message;

public class EntityAnimationEvent : MonoBehaviour
{
    public readonly Message AttackMessage = new Message();

    public void AttackEvent()
    {
        AttackMessage.Send();
    }
}
