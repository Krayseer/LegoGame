using UnityEngine;
using UnityEngine.EventSystems;

namespace Play
{
    public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            CameraRotateAround.direction = int.Parse(tag);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CameraRotateAround.direction = 0;
        }
    }
}
