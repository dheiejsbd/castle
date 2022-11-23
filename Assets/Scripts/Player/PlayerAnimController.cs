using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FrameWork.Player;
using Random = UnityEngine.Random;

namespace Player
{
    class PlayerAnimController:PlayerComponent
    {
        Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Attack()
        {
            animator.SetInteger("AttackID", Random.Range(0, animator.GetInteger("AttackCount")));
            animator.SetTrigger("attack");
        }
        public void Hit()
        {
            animator.SetTrigger("hit");
        }

        public void SeeLeft()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        public void SeeRight()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
