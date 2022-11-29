using UnityEngine;
using UnityEngine.SceneManagement;

namespace Music
{
    public class StartGame : MonoBehaviour
    {
        public static bool startMusic = false;

        private void Awake()
        {
            if (!startMusic)
            {
                startMusic = true;
                SceneManager.LoadScene(2);
                SceneManager.LoadScene(0);
            }
        }
    }
}
