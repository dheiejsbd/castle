using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace FrameWork.Page
{
    public class PageManager
    {
        IPage activePage;
        Dictionary<int, IPage> pages = new Dictionary<int, IPage>();
        bool isLoaded = false;
        public void Start()
        {
            AddPage(new LobbyPage(this));
            AddPage(new InGamePage(this));
            ChangePage(PageID.Lobby);
        }
        public void Update()
        {
            if (!isLoaded) return;
            activePage?.Update();
        }


        public void ChangePage(PageID pageID)
        {
            ChangePage((int)pageID);
        }
        public void ChangePage(int pageID)
        {
            Coroutine.instance.StartCor(ChangePageCor(pageID));
        }

        IEnumerator ChangePageCor(int pageID)
        {
            isLoaded = false;
            if(activePage != null) yield return activePage.Exit();
            activePage = pages[pageID];
            yield return activePage.Prepare();
            yield return null;
            activePage.Enter();
            isLoaded = true;
        }

        public void AddPage(IPage page)
        {
            if (pages.ContainsKey(page.ID)) return;
            page.Initialize();
            pages.Add(page.ID, page);
        }
        public void RemovePage(IPage page)
        {
            if (!pages.ContainsKey(page.ID)) return;
            pages.Remove(page.ID);
        }
    }
}