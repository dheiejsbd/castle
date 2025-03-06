using System.Collections;
using UnityEngine;
using FrameWork.UI;
using UnityEngine.UI;

public class InGameWindow : UIWidget
{
    [SerializeField] Image ProgressBar;
    [SerializeField] DelayProgressBar PlayerHpBar;
    public void SetPrograss(float amount)
    {
        ProgressBar.fillAmount = amount;
    }

    public void SetHp(float amount)
    {
        PlayerHpBar.SetProgress(amount);
    }



}
