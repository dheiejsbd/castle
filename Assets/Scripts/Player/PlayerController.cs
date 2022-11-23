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
        bool canAttack = true;
        const float AttackDist = 0.15f * 5;
        const float AttackOffset = 0.05f * 5;
        const float AttackCoolTime = 0.125f;

        public event Action<Entity> AttackSuccess;
        [SerializeField] ParticleSystem flash;

        [SerializeField]AudioClip attackSound;
        [SerializeField]AudioClip attackSuccessSound;

        public readonly Message<Entity> HitMessage = new Message<Entity>();
        public Func<bool, Entity> GetNearByPlayerMonster;
        public bool death { get; private set; }

        public void OnStart()
        {
            input = new PlayerInput();
            animController = gameObject.AddComponent<PlayerAnimController>();

            input.LeftAttack.AddListener(() => TryAttack(true));
            input.RightAttack.AddListener(() => TryAttack(false));

            AttackSuccess += (Entity con) => SoundManager.instance.PlayEffect(attackSuccessSound);
            AttackSuccess += (Entity con) => con.Hit();
            AttackSuccess += (Entity con) =>
            {
                flash.transform.position = con.gameObject.transform.position;
                flash.Play();
            };
            HitMessage.AddListener(Hit);
        }

        public void OnUpdate()
        {
            if(death) return;
            input.Update();
        }

        void Hit(Entity entity)
        {
            Debug.Log("player death");
            death = true;
            animController.Hit();
        }

        void TryAttack(bool left) 
        {
            if (!canAttack) return;
            Attack(left);
        }
        void Attack(bool left)
        {
            SoundManager.instance.PlayEffect(attackSound);
            canAttack = false;
            Coroutine.instance.Timer(AttackCoolTime, () => canAttack = true);
            animController.Attack();
            if (left)
            {
                animController.SeeLeft();
            }
            else
            {
                animController.SeeRight();
            }
            var controller = GetNearByPlayerMonster(left);


            if(controller != null)
            {
                if(Mathf.Abs(Mathf.Abs(controller.gameObject.transform.position.x) - AttackDist) <= AttackOffset)
                {
                    AttackSuccess?.Invoke(controller);
                }
            }
        }
    }
}
