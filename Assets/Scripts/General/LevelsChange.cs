using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class LevelsChange : MonoBehaviour
    {
        [Header("Buttons")]
        public GameObject nextButton;
        public GameObject prevButton;

        [Header("Canvas")]
        public GameObject canvas;

        private int _page = 1;
        private readonly List<string> _tags = new() { "1", "2", "3", "4", "5" };
        private Transform[] _temp;


        private void Start()
        {
            _temp = canvas.GetComponentsInChildren<Transform>();
            Script(true, false, false);
        }

        private void Update()
        {
            switch (_page)
            {
                case 1:
                    prevButton.SetActive(false);
                    break;
                case 3:
                    nextButton.SetActive(false);
                    break;
                default:
                    nextButton.SetActive(true);
                    prevButton.SetActive(true);
                    break;
            }
        }

        public void NextPage() => Script(false, true, false);

        public void PrevPage() => Script(false, false, true);

        private void Script(bool start, bool next, bool prev)
        {
            if (start)
                foreach (var k in _temp)
                    if (_tags.Contains(k.tag) && !k.CompareTag("1"))
                        k.gameObject.SetActive(false);
            if (next)
            {
                _page++;
                foreach (var k in _temp)
                {
                    if (k.CompareTag(_page.ToString()))
                        k.gameObject.SetActive(true);
                    else if (k.CompareTag((_page - 1).ToString()))
                        k.gameObject.SetActive(false);
                }
            }

            else if (prev)
            {
                _page--;
                foreach (var k in _temp)
                {
                    if (k.CompareTag(_page.ToString()))
                        k.gameObject.SetActive(true);
                    else if (k.CompareTag((_page + 1).ToString()))
                        k.gameObject.SetActive(false);
                }
            }
        }
    }
}
