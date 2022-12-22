using UnityEngine;

namespace Play
{
    public class HintController : MonoBehaviour
    {
        private int countPeek;

        public void HitChange()
        {
            if (countPeek++ % 2 != 0)
                CreateSketch.Instance.CreateHint();
            else
                CreateSketch.Instance.RemoveHint();
        }
    }
}
