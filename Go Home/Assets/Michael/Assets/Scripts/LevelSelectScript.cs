using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class LevelSelectScript : MonoBehaviour
{
    [SerializeField] InfoRetainer retainer;
    public UnityEvent unityEvent;
    public GameObject button;
    private Vector3 storeSize;
    public string levelName, levelSelect;
    public int completedLevel;
    private AudioSource audio;
    GUIStyle style;
    string text, labelText;
    // Start is called before the first frame update
    void Start()
    {

        audio = GetComponent<AudioSource>();
        button = this.gameObject;
        storeSize = transform.localScale;
        levelName = this.gameObject.name;
        completedLevel = retainer.levelCompleted;

        //style for on-screen text
        text = "";
        style = new GUIStyle();
        style.fontSize = 45;
        style.normal.textColor = Color.cyan;
        style.alignment = TextAnchor.MiddleCenter;
        style.font = Resources.Load<Font>("Kenney Future");


    }

    // Update is called once per frame
    void Update()
    {
        LevelSelector();
        transform.Rotate(0, -10 * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(completedLevel);
        }

        //checks is mouse hovers over planet and increases the size
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
          
            text = labelText;
            if (transform.localScale.x <= 1f)
            {              
                transform.localScale += new Vector3(.1f, .1f, .1f);
                audio.Play();
            }
            //if clicked, change levels
            if (Input.GetMouseButtonDown(0))
            {
                GameObject.FindGameObjectWithTag("Music").GetComponent<KeepMusic>().StopAudio();
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<ButtonAction>()._levelSelect = levelSelect;
                unityEvent.Invoke();
            }
        }
        //otherwise go back to normal size and remove text
        else
        {
            transform.localScale = storeSize;
            text = "";
        }
    }

        //checks which level is hovered over and adjusts text to match 
        void LevelSelector()
        {
            switch (levelName)
            {
                case "Level 1":
                    labelText = "Stage 1: Malrion";
                    levelSelect = "Go Home LvL 1";
                    break;
                case "Level 2":
                    labelText = "Stage 2: Mauthea 04B";
                    levelSelect = "Go Home LvL 2";
                    break;
                case "Level 3":
                    labelText = "Stage 3: Druvoter";
                    levelSelect = "Go Home LvL 3";
                    break;
                default:
                    break;
            }
        }

       private void OnGUI()
        {
            Rect rect = new Rect(0, Screen.height - 100, Screen.width, 120);
            Rect rect2 = new Rect(Screen.width / 2 - 100, -45, Screen.width, 120);
            GUI.Label(rect, text, style);
        }

    }

