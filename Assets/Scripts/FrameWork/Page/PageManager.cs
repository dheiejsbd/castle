using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Page
{
    public class PageManager
    {
        IPage activePage;
        Dictionary<int, IPage> pages = new Dictionary<int, IPage>();

        public void Start()
        {
            AddPage(new Lobby());
            AddPage(new InGame());
            ChangePage(PageID.Ingame);
        }
        public void Update()
        {
            activePage?.Update();
        }


        public void ChangePage(PageID pageID)
        {
            ChangePage((int)pageID);
        }
        public void ChangePage(int pageID)
        {
            activePage?.Exit();
            activePage = pages[pageID];
            activePage.Enter();
        }

        public void AddPage(IPage page)
        {
            if (pages.ContainsKey(page.ID)) return;
            pages.Add(page.ID, page);
        }
        public void RemovePage(IPage page)
        {
            if (!pages.ContainsKey(page.ID)) return;
            pages.Remove(page.ID);
        }
    }
}