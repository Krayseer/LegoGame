using General;
using UnityEngine;

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


        void Start()
        {
            invisibleMaterial = Resources.Load("Materials/invisible", typeof(Material)) as Material;
            hintMaterial = Resources.Load("Materials/hint", typeof(Material)) as Material;

            childrens = GetComponentsInChildren<Transform>();
            defaultMaterials = new Material[childrens.Length];

            for(var number = 1; number < childrens.Length; number++)
            {
                defaultMaterials[number] = childrens[number].gameObject.GetComponent<MeshRenderer>().material;
                childrens[number].gameObject.GetComponent<MeshRenderer>().material = invisibleMaterial;
                childrens[number].gameObject.AddComponent<Child>();
                if(number > 1)
                    childrens[number].gameObject.SetActive(false);
            }

            spawnPosition = new Vector3(-20.7f, 28f, -46.6f);
            Instance = this;
        }

        void Update()
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
                    createChild = true;
                }
                else
                {
                    endGame = true;
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
    }
}
