using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.Page
{
    public interface IPage
    {
        int ID { get; }
        void Initialize();
        IEnumerator Prepare();
        void Enter();
        void Update();
        IEnumerator Exit();
    }
}