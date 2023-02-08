using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    private GameObject iconManager;

    void Start()
    {
        //Find the iconmanager
        iconManager = GameObject.Find("MinimapIcons");
        CreateIcon();
    }

    void Update()
    {
        //The icon stays above the gameobject
        icon.transform.position = new Vector3(transform.position.x, transform.position.y + 8, transform.position.z);
    }

    //Instantiate the icon above the gameobject this is attached to
    private void CreateIcon()
    {
        icon = Instantiate(icon, iconManager.transform);
        icon.transform.position = new Vector3(transform.position.x, transform.position.y + 8, transform.position.z);
    }
    //Call this method to destroy the icon when no longer necessairy
    internal void DeleteIcon()
    {
        GameObject.Destroy(icon);
    }
}
