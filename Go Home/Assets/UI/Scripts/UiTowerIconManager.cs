using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTowerIconManager : MonoBehaviour
{
    [SerializeField]
    private UiTowerIconActivation[] iconActivation;

    private void Start()
    {
        iconActivation = GetComponentsInChildren<UiTowerIconActivation>();
    }

    private void Update()
    {
    }

    public void Activate()
    {
        foreach (UiTowerIconActivation icon in iconActivation)
        {
            if (!icon.activeIcon)
            {
                icon.activeIcon = true;
                return;
            }
        }
    }

    public void Deactivate()
    {
        for (int i = iconActivation.Length - 1; i > -1; i--)
        {
            if(iconActivation[i].activeIcon)
            {
                iconActivation[i].activeIcon = false;
                return;
            }
        }
    }
}
