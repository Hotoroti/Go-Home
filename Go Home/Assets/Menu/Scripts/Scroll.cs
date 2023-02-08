using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private float scrollSpeed = 18f;
    private Vector2 scrollLimits;

    private void Start()
    {
        scrollLimits = new Vector2(220, Screen.height * 2);        
    }

    void Update()
    {
        //If you scroll down the credits will move down, otherwise it will move up
        if (Input.mouseScrollDelta.y < 0)
        {
            transform.position += new Vector3(0, scrollSpeed, 0);
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            transform.position += new Vector3(0, -scrollSpeed, 0);
        }

        //Clamp the position between the scroll limits
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, scrollLimits.x, scrollLimits.y), transform.position.z);
    }
}
