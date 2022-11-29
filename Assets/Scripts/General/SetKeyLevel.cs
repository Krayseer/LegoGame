using UnityEngine;

namespace General
{
    public class SetKeyLevel : MonoBehaviour
    {
        public void SetKey(string key) => PlayerPrefs.SetString("level", key);
    }
}
