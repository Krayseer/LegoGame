using UnityEngine;
using UnityEngine.UI;

namespace Music
{
    public class SliderMusicScript : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Slider slider;
        [SerializeField] private AudioSource audioSource;

        [Header("Keys")]
        [SerializeField] private string saveVolumeKey;

        [Header("Tags")]
        [SerializeField] private string sliderTag;

        [Header("Parameters")]
        [SerializeField] private float volume;

        private void LateUpdate()
        {
            var sliderObj = GameObject.FindWithTag(sliderTag);
            if(sliderObj != null)
            {
                slider = sliderObj.GetComponent<Slider>();
                volume = slider.value;

                if (audioSource.volume != volume)
                    PlayerPrefs.SetFloat(saveVolumeKey, volume);
            }
            audioSource.volume = volume;
        }

        private void Awake()
        {
            if (PlayerPrefs.HasKey(saveVolumeKey))
            {
                volume = PlayerPrefs.GetFloat(saveVolumeKey);
                audioSource.volume = volume;

                var sliderObj = GameObject.FindWithTag(sliderTag);
                if(sliderObj != null)
                {
                    slider = sliderObj.GetComponent<Slider>();
                    slider.value = volume;
                }
            }
            else
            {
                volume = 0.5f;
                PlayerPrefs.SetFloat(saveVolumeKey, volume);
                audioSource.volume = volume;
            }
        }
    }
}
