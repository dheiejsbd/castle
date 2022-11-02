using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Player;
namespace FrameWork.Player
{
    class PlayerComponent:MonoBehaviour
    {
        protected PlayerController Player { 
            get
            {
                if (instance == null) instance = gameObject.GetComponent<PlayerController>();
                return instance;
            }
        }
        PlayerController instance;
    }
}
