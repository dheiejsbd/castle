using DG.Tweening;
using UnityEngine;

namespace FrameWork.UI
{
    public class UIWidget : MonoBehaviour
    {
        public virtual Sequence ShowSequence
        {
            get
            {
                Sequence seq = DOTween.Sequence()
                .OnStart(() =>
                {
                    Activate();
                    Debug.Log("보여주기 시퀀스 시작");
                })
                .OnComplete(() =>
                {
                    Debug.Log("보여주기 시퀀스 종료");
                });

                return seq;
            }
        }

        public virtual Sequence HideSequence
        {
            get
            {
                Sequence seq = DOTween.Sequence()
                .OnStart(() =>
                {
                    Debug.Log("숨기기 시퀀스 시작");
                })
                .OnComplete(() =>
                {

                    Deactivate();
                    Debug.Log("숨기기 시퀀스 종료");
                });

                return seq;
            }
        }

        protected virtual void Awake() { }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}