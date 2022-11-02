using FrameWork.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerController:MonoBehaviour
    {
        PlayerInput input;
        PlayerAnimController animController;
        float nextAttackTime;
        const float AttackDist = 0.878f;
        const float AttackTime = 0.2f;

        event Action<Entity> AttackSuccess;
        event Action AttackFailed;

        public readonly Message<Entity> HitMessage = new Message<Entity>();
        public bool death { get; private set; }
        public void Start()
        {
            input = new PlayerInput();
            animController = gameObject.AddComponent<PlayerAnimController>();

            input.LeftAttack.AddListener(() => TryAttack(true));
            input.RightAttack.AddListener(() => TryAttack(false));

            AttackSuccess += (Entity con) => con.Hit();
            HitMessage.AddListener(Hit);
        }

        private void Update()
        {
            if(death) return;
            input.Update();
            nextAttackTime -= Time.deltaTime;
        }

        void Hit(Entity entity)
        {
            death = true;
            animController.Hit();
        }

        void TryAttack(bool left)
        {
            if (!CanAttack()) return;
            Attack(left);
        }
        void Attack(bool left)
        {
            nextAttackTime = AttackTime;
            animController.Attack();
            if (left)
            {
                animController.SeeLeft();
            }
            else
            {
                animController.SeeRight();
            }
            var controller = GameManager.instance.monsterManager.GetNearByPlayerMonster(left);


            if(controller == null)
            {
                AttackFailed?.Invoke();
            }
            else
            {
                if(Mathf.Abs(controller.gameObject.transform.position.x) <= AttackDist)
                {
                    AttackSuccess?.Invoke(controller);
                }
                else
                {
                    AttackFailed?.Invoke();
                }
            }
        }

        bool CanAttack()
        {
            if (nextAttackTime > 0) return false;
            return true;
        }
    }
}
