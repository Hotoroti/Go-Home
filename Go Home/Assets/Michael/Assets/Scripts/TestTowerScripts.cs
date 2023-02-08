using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTowerScripts : MonoBehaviour
{

    public List<GameObject> testlist = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(testlist.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            testlist.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            testlist.Remove(other.gameObject);
        }
    }
}
