using UnityEngine;
using UnityEngine.UI;

namespace Play
{
    public class ChangeSize : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private Slider slider;

        private const float INDEX = 45;
        private const float STARTINDEX = 65;

        private int count;

        void Update() => camera.fieldOfView = STARTINDEX - slider.value * INDEX;

        public void OnButton() => slider.gameObject.SetActive(count++ % 2 == 0);
    }
}
