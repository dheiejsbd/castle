using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FrameWork.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class UIButton : UIWidget, IPointerDownHandler, IPointerUpHandler
    {
        public System.Action onButtonWasPressed;
        public System.Action onButtonisPressing;
        public System.Action onButtonWasReleased;


        float LastTriggerTime = 0;
        bool isPressed = false;


        protected override void Awake() 
        {
            UnityEngine.UI.Button ButtonComponent = gameObject.GetComponent<UnityEngine.UI.Button>();
        }

        protected void Update()
        {
            if (isPressed)
                onButtonisPressing?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPressed = true;

            onButtonWasPressed?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false; 

            onButtonWasReleased?.Invoke();;
        }
    }
}