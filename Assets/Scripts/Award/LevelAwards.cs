using System;
using System.Linq;
using UnityEngine;

namespace Award
{
    public class LevelAwards : MonoBehaviour
    {
        private void Update()
        {
            foreach(var tf in gameObject.GetComponentsInChildren<Transform>())
                if (tf.name.Equals("Complete") && !PlayerPrefs.GetString("awards").Split(';').Contains(tf.tag))
                    tf.gameObject.SetActive(false);
        }
    }
}
