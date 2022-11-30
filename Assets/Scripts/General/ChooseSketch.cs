using Play;
using UnityEngine;

namespace General
{
    public class ChooseSketch : MonoBehaviour
    {
        private void Start()
        {
            foreach (Transform form in transform)
            {
                if(PlayerPrefs.GetString("level") != form.name)
                    form.gameObject.SetActive(false);
                else
                    form.gameObject.AddComponent<CreateSketch>();
            }
        }
    }
}
