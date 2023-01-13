using UnityEngine;

namespace Award
{
    public class Awards : MonoBehaviour
    {
        private void Start()
        {
            foreach (Transform form in transform)
                if (!PlayerPrefs.GetString("awards").Contains(form.name) && !form.CompareTag("info"))
                    form.gameObject.SetActive(false);
        }
    }
}
