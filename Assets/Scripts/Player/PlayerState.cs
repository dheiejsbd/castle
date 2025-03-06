using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerState", menuName = "ScriptableObject/Player/State", order = int.MaxValue)]
    public class PlayerState: ScriptableObject
    {
        public int Health;
        public int Armor;
        public int ChargeSpeed;
    }
}
