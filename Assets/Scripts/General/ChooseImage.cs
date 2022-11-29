using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseImage : MonoBehaviour
{
    private Transform[] currentImage;

    public static ChooseImage Instance;
    
    private void Start()
    {
        foreach (Transform form in transform)
        {
            if (PlayerPrefs.GetString("level") != form.name)
                form.gameObject.SetActive(false);
            else
                currentImage = form.GetComponentsInChildren<Transform>();
        }

        for (var index = 2; index < currentImage.Length; index++)
        {
            currentImage[index].gameObject.SetActive(false);
        }

        Instance = this;
    }

    public void SetNextImage(int index)
    {
        currentImage[index].gameObject.SetActive(true);
    }
}
