using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    private float fadeSpeed = 1f;
    private RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
    }

    public void Update()
    {
        image.color = newColor(image.color);

        if (image.color.a <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private Color newColor(Color color)
    {
        color.a -= fadeSpeed * Time.deltaTime;
        Debug.Log("Test");
        return color;
    }
}
