using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScaling : MonoBehaviour
{
    [SerializeField]
    private RectTransform border;
    [SerializeField]
    private RectTransform map;

    private void Start()
    {
        map = GetComponent<RectTransform>();
        border = GetComponentsInParent<RectTransform>()[1];
    }

    private void Update()
    {
        map.sizeDelta = new Vector2(Screen.width/5, Screen.width/5);
        border.sizeDelta = map.sizeDelta + new Vector2(map.sizeDelta.x/20, map.sizeDelta.y/20);

        border.SetPositionAndRotation(new Vector3(border.sizeDelta.x / 2, (-border.sizeDelta.y / 2) + Screen.height, 0), Quaternion.identity);
    }
}
