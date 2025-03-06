using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using FrameWork.Page;
using FrameWork.Loading;
using DG.Tweening;
using UnityEngine.EventSystems;

public class LobbyPage : Page
{
    public override int ID => (int)PageID.Lobby;
    LobbyWindow LobbyWindow;

    public LobbyPage(PageManager pageManager) : base(pageManager)
    {

    }

    public override void Initialize()
    {
        LobbyWindow = UIManager.instance.GetWindow(typeof(LobbyWindow)) as LobbyWindow;
    }
    public override IEnumerator Prepare()
    {
        yield return LoadingProcess.instance.LoadScene("Lobby");
    }
    public override void Enter()
    {
        LobbyWindow.ShowSequence.Play();
    }
    public override void Update()
    {
        foreach (var item in Input.touches)
        {
            if (item.phase != TouchPhase.Began) continue;
            if (EventSystem.current.IsPointerOverGameObject(item.fingerId)) continue;

            pageManager.ChangePage(PageID.Ingame);
            return;
        }
    }
    public override IEnumerator Exit()
    {
        LobbyWindow.HideSequence.Play();
        yield return LoadingProcess.instance.UnloadScene("Lobby");
    }
}