using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace FrameWork.UI
{
    [RequireComponent(typeof(Image))]
    public class DelayProgressBar : UIWidget
    {
        [SerializeField] Image progressImage;
        [SerializeField] Image delayProgressImage;
        [SerializeField] float animTime;
        [SerializeField] float delay;
        [SerializeField] Ease lerpEase;

        [SerializeField] Ease subDelayEase;
        [SerializeField] Ease addDelayEase;
        [SerializeField] Color AddColor;
        [SerializeField] Color SubColor;
        public float targetProgress;
        Sequence sequence;
        bool lastActAdd = true;
        public void SetProgress(float progress)
        {
            Delay(progress);
            targetProgress = progress;
        }

        void Delay(float progress)
        {
            sequence.Kill();
            sequence = DOTween.Sequence();

            bool add = targetProgress < progress;
            if (add)
            {
                //증가
                delayProgressImage.color = AddColor;
                if (!lastActAdd)
                {
                    progressImage.fillAmount = targetProgress;
                    sequence.Append(DOTween.To(() => progressImage.fillAmount, x => progressImage.fillAmount = x, targetProgress, 0));
                }
                sequence.Append(DOTween.To(() => delayProgressImage.fillAmount, x => delayProgressImage.fillAmount = x, progress, 0));
                sequence.AppendInterval(delay);
                sequence.Append(DOTween.To(() => progressImage.fillAmount, x => progressImage.fillAmount = x, progress, animTime).SetEase(lerpEase));
                sequence.Play();
            }
            else
            {
                //감소
                delayProgressImage.color = SubColor;
                if (lastActAdd)
                {
                    progressImage.fillAmount = targetProgress;
                    sequence.Append(DOTween.To(() => progressImage.fillAmount, x => progressImage.fillAmount = x, targetProgress, 0));
                }
                sequence.Append(DOTween.To(() => progressImage.fillAmount, x => progressImage.fillAmount = x, progress, 0));
                sequence.AppendInterval(delay);
                sequence.Append(DOTween.To(() => delayProgressImage.fillAmount, x => delayProgressImage.fillAmount = x, progress, animTime).SetEase(lerpEase));
                sequence.Play();
            }
            lastActAdd = add;
        }
    }
}