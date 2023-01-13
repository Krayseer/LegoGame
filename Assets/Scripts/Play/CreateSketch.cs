using System.Collections;
using System.Linq;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace Play
{
    public class CreateSketch : MonoBehaviour
    {
        private Material invisibleMaterial;
        private Material hintMaterial;
        private Material[] defaultMaterials;

        private Transform[] childrens;
        private GameObject childReference;

        public static CreateSketch Instance;

        private Vector3 spawnPosition;

        private int index = 0;
        private bool createChild = false;
        private bool endGame = false;

        private GameObject finish;
        private GameObject info;
        private GameObject infoButton;

        private void Start()
        {
            invisibleMaterial = Resources.Load("Materials/invisible", typeof(Material)) as Material;
            hintMaterial = Resources.Load("Materials/hint", typeof(Material)) as Material;

            childrens = GetComponentsInChildren<Transform>();
            defaultMaterials = new Material[childrens.Length];

            for(var number = 1; number < childrens.Length; number++)
            {
                defaultMaterials[number] = childrens[number].gameObject.GetComponent<MeshRenderer>().material;
                childrens[number].gameObject.GetComponent<MeshRenderer>().material = hintMaterial;
                childrens[number].gameObject.AddComponent<Child>();
                if(number > 1)
                    childrens[number].gameObject.SetActive(false);
            }

            spawnPosition = new Vector3(-20.7f, 28f, -46.6f);
            Instance = this;
            
            finish = GameObject.FindGameObjectWithTag("finish");
            finish.SetActive(false);

            info = GameObject.FindGameObjectWithTag("info");
            var path = "Info/" + PlayerPrefs.GetString("level");
            info.GetComponent<RawImage>().texture = Resources.Load<Texture>(path);
            foreach (Transform button in info.transform)
                infoButton = button.gameObject;
            infoButton.SetActive(false);
        }

        private void Update()
        {
            if (!createChild)
            {
                if(index != childrens.Length - 1)
                {
                    var newChild = Instantiate(childrens[++index].gameObject, spawnPosition, childrens[index].rotation);
                    Destroy(newChild.GetComponent<Child>());
                    ChooseImage.Instance.SetNextImage(index);

                    var size = childrens[index].GetComponent<BoxCollider>().size;
                    childrens[index].GetComponent<BoxCollider>().size = new Vector3(size.x / 100, size.y / 100, size.z / 100);

                    newChild.AddComponent<Rigidbody>();
                    newChild.GetComponent<Rigidbody>().isKinematic = false;
                    newChild.GetComponent<Rigidbody>().freezeRotation = true;

                    newChild.GetComponent<BoxCollider>().isTrigger = false;
                    newChild.GetComponent<MeshRenderer>().material = defaultMaterials[index];

                    childReference = newChild;
                    if (index == childrens.Length - 2)
                        StartInvisible();
                    createChild = true;
                }
                else
                {
                    finish.SetActive(true);
                    endGame = true;
                    createChild = true;
                    SaveProgress();
                }
            }
        }

        public void NextIteration()
        {
            childReference.SetActive(false);
            childrens[index].GetComponent<MeshRenderer>().material = defaultMaterials[index];
            if (index != childrens.Length - 1)
                childrens[index + 1].gameObject.SetActive(true);
            createChild = false;
        }

        public void CreateHint()
        {
            if(!endGame)
                childrens[index].GetComponent<MeshRenderer>().material = hintMaterial;
        }

        public void RemoveHint()
        {
            if(!endGame)
                childrens[index].GetComponent<MeshRenderer>().material = invisibleMaterial;
        }

        private static void SaveProgress()
        {
            if (!PlayerPrefs.HasKey("awards"))
                PlayerPrefs.SetString("awards", PlayerPrefs.GetString("level"));
            else
                if (!PlayerPrefs.GetString("awards").Split(';').Contains(PlayerPrefs.GetString("level")))
                    PlayerPrefs.SetString("awards", PlayerPrefs.GetString("awards") + ';' +
                                                    PlayerPrefs.GetString("level"));

            var award = GameObject.FindGameObjectWithTag("award");
            var path = "Awards/" + PlayerPrefs.GetString("level");
            award.GetComponent<RawImage>().texture = Resources.Load<Texture>(path);
        }
        
        IEnumerator Invisible()
        {
            for (float f = 0; f <= 2; f += 0.05f)
            {
                Color color = info.GetComponent<RawImage>().color;
                color.a = f;
                info.GetComponent<RawImage>().color = color;
                yield return new WaitForSeconds(0.15f);
            }
            infoButton.SetActive(true);
        }
        
        public void StartInvisible() => StartCoroutine(nameof(Invisible));
    }
}
