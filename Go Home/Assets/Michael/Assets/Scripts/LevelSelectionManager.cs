using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    [SerializeField] InfoRetainer retainer;
    [SerializeField] GameObject hiddenLevel;
    private int lastCompletedLevel;
    private GameObject level1, level2, level3, secretLevel, _hiddenLevel;
    // Start is called before the first frame update
    void Start()
    {
        //keep audio between menu selection
        GameObject.FindGameObjectWithTag("Music").GetComponent<KeepMusic>().PlayAudio();
        //check for last completed level in the info retainer
        lastCompletedLevel = retainer.levelCompleted;

        level1 = GameObject.Find("Level 1");
        level2 = GameObject.Find("Level 2");
        level3 = GameObject.Find("Level 3");
        planetManager();
    }

    //check which level has been completed last,
    //then either adds the hidden level prefab or enables the level to be clicked. adjusts position for the final teaser
    void planetManager()
    {
        switch (lastCompletedLevel)
        {
            case 0:
                _hiddenLevel = Instantiate(hiddenLevel, new Vector3(level2.transform.position.x, level2.transform.position.y, level2.transform.position.z), Quaternion.identity);
                level2.SetActive(false);
                _hiddenLevel = Instantiate(hiddenLevel, new Vector3(level3.transform.position.x, level3.transform.position.y, level3.transform.position.z), Quaternion.identity);
                level3.SetActive(false);
                break;

            case 1:
               _hiddenLevel = Instantiate(hiddenLevel, new Vector3(level3.transform.position.x, level3.transform.position.y, level3.transform.position.z), Quaternion.identity);
               level3.SetActive(false);
                break;

            case 2:
                break;

            case 3:
                _hiddenLevel = Instantiate(hiddenLevel, new Vector3(level3.transform.position.x+.5f, level3.transform.position.y, level3.transform.position.z), Quaternion.identity);
                level1.transform.position = new Vector3(level1.transform.position.x - 1, level1.transform.position.y, level1.transform.position.z);
                level2.transform.position = new Vector3(level2.transform.position.x - 1.33f, level2.transform.position.y, level2.transform.position.z);
                level3.transform.position = new Vector3(level3.transform.position.x - 1.66f, level3.transform.position.y, level3.transform.position.z);
                break;

            default:
                _hiddenLevel = Instantiate(hiddenLevel, new Vector3(level2.transform.position.x, level2.transform.position.y, level2.transform.position.z), Quaternion.identity);
                level2.SetActive(false);
                _hiddenLevel = Instantiate(hiddenLevel, new Vector3(level3.transform.position.x, level3.transform.position.y, level3.transform.position.z), Quaternion.identity);
                level3.SetActive(false);
                break;
        }
    }
}
