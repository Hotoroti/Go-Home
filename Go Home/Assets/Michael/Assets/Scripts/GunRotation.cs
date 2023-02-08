using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunRotation : MonoBehaviour
{
    private TowerTargeting tower;
    private Vector3 targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        tower = GetComponentInParent<TowerTargeting>();
    }

    // Update is called once per frame
    void Update()
    {
        //make gun look at where the tower tells it has to look at
        targetPoint = tower._targetPoint;
        transform.LookAt(targetPoint);

        //clamping rotation so gun doesn't phase through tower
        Vector3 rot = transform.eulerAngles;
        rot.x = Mathf.Clamp(rot.x, 0, 50f);
        transform.eulerAngles = rot;
        
    }
}
