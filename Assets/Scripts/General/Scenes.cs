using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class Scenes : MonoBehaviour
    {
        public void NextLevel(int numberScene) => SceneManager.LoadScene(numberScene);

        public void ExitGame() => Application.Quit();
    }
}
