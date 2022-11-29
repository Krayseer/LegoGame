using UnityEngine;
using UnityEngine.UI;

namespace Template.ColorSkies.Demo.Scripts
{
    public class SkyboxChanger : MonoBehaviour
    {
        public Material[] Skyboxes;
        private Dropdown _dropdown;

        public void Awake()
        {
            _dropdown = GetComponent<Dropdown>();
            //var options = Skyboxes.Select(skybox => skybox.name).ToList();
            //_dropdown.AddOptions(options);
        }

        public void ChangeSkybox()
        {
            RenderSettings.skybox = Skyboxes[_dropdown.value];
        }
    }
}