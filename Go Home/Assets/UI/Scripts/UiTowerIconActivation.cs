using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTowerIconActivation : MonoBehaviour
{
    public bool activeIcon;

    private Color activeColor;
    private Color inactiveColor;
    private Image icon;


    private void Start()
    {
        activeIcon = true;
        activeColor = Color.white;
        activeColor.a = 1f;
        inactiveColor = Color.gray;
        inactiveColor.a = 0.5f;

        icon = GetComponent<Image>();
    }

    private void Update()
    {
        if (activeIcon)
        {
            icon.color = activeColor;
        }
        else if (!activeIcon)
        {
            icon.color = inactiveColor;
        }
    }
}
