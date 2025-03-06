using DG.Tweening;
using UnityEngine;

namespace FrameWork.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWidget : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        public readonly float FadeTime = 0.5f;
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
                .Append(canvasGroup.DOFade(1,FadeTime))
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
                    Activate();
                    Debug.Log("숨기기 시퀀스 시작");
                })
                .Append(canvasGroup.DOFade(0,FadeTime))
                .OnComplete(() =>
                {
                    Deactivate();
                    Debug.Log("숨기기 시퀀스 종료");
                });

                return seq;
            }
        }

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

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