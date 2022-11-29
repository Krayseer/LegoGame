using UnityEngine;

namespace Music
{
    public class MusicBackground : MonoBehaviour
    {
        [Header("Tags")]
        public string createTag;

        private void Awake()
        {
            GameObject obj = GameObject.FindWithTag(createTag);
            if(obj != null)
                Destroy(gameObject);
            else
            {
                gameObject.tag = createTag;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
