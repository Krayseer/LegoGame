using UnityEngine;

namespace Play
{
    public class Child : MonoBehaviour
    {
        private void OnTriggerEnter(Collider child)
        {
            if (CompareTag(child.tag))
                CreateSketch.Instance.NextIteration();
        }
    }
}
