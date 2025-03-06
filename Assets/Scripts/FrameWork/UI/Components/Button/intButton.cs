using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


namespace FrameWork.UI
{
    public class intButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] int value;
        private bool isBtnDown = false;
        public delegate void Deligate(int id);
        Deligate PointerUpdate;
        Deligate PointerUp;
        Deligate PointerDown;

        void Update()
        {
            if (isBtnDown)
            {
                if (PointerUpdate != null)
                    PointerUpdate(value);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (PointerDown != null)
                PointerDown(value);
            isBtnDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (PointerUp != null)
                PointerUp(value);
            isBtnDown = false;
        }

        public void AddPointerUpdate(Deligate deligate)
        {
            PointerUpdate += deligate;
        }

        public void AddPointerDown(Deligate down)
        {
            PointerDown += down;
        }

        public void AddPointerUp(Deligate up)
        {
            PointerUp += up;
        }
    }
}