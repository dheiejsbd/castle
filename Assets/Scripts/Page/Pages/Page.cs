using System.Collections;
using UnityEngine;
using FrameWork.Page;
public abstract class Page : IPage
{
    protected PageManager pageManager;

    public Page(PageManager pageManager)
    {
        this.pageManager = pageManager;
    }

    public abstract int ID { get; }

    public abstract void Initialize();
    public abstract IEnumerator Prepare();
    public abstract void Enter();
    public abstract void Update();
    public abstract IEnumerator Exit();
}
