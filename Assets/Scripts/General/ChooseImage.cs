using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class ChooseImage : MonoBehaviour
    {
        private Transform[] currentImage;

        private Texture[] textures;

        public static ChooseImage Instance;
    
        private void Start()
        {
            var lvlName = PlayerPrefs.GetString("level");
        
            textures = Sort(Resources.LoadAll("Images/" + lvlName, typeof(Texture)).Cast<Texture>().ToArray());
        
            foreach (Transform form in transform)
            {
                if (lvlName != form.name)
                    form.gameObject.SetActive(false);
                else
                    currentImage = form.GetComponentsInChildren<Transform>();
            }

            for (var index = 1; index < currentImage.Length; index++)
            {
                currentImage[index].gameObject.GetComponent<RawImage>().texture = textures[index - 1];
                if(index > 1)
                    currentImage[index].gameObject.SetActive(false);
            }

            Instance = this;
        }

        public void SetNextImage(int index) => currentImage[index].gameObject.SetActive(true);

        private static Texture[] Sort(Texture[] arr)
        {
            for (var write = 0; write < arr.Length; write++) 
                for (var sort = 0; sort < arr.Length - 1; sort++)
                    if (int.Parse(arr[sort].name) > int.Parse(arr[sort + 1].name))
                        (arr[sort], arr[sort + 1]) = (arr[sort + 1], arr[sort]);
            return arr;
        }
    }
}
