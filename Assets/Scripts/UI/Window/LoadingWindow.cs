using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.UI;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow: UIWidget
{
    [SerializeField] Image prograssBar;

    public void SetPrograss(float prograss)
    {
        prograssBar.fillAmount = prograss;
    }
}