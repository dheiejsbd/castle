using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FrameWork.Player;

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
