using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI.MessageBox;

public class TutorialButton : MonoBehaviour
{
    public UnityEvent unityEvent;
    public GameObject button;
    private Vector3 storeSize;
    public string levelSelect;
    public int completedLevel;
    private AudioSource audio;
    GUIStyle style;
    string text, labelText;

    // Start is called before the first frame update
    void Start()
    {
        text = "";
        style = new GUIStyle();
        audio = GetComponent<AudioSource>();
        button = this.gameObject;
        storeSize = transform.localScale;
        levelSelect = "Tutorial";
        style.fontSize = 45;
        style.normal.textColor = Color.cyan;
        style.alignment = TextAnchor.MiddleCenter;
        style.font = Resources.Load<Font>("Kenney Future");
    }

    // Update is called once per frame
    void Update()
    {
        //same thing as in level select, but adjusted for the different size of the game object. could have been added in the other script,
        //this was done for easier readability
        transform.Rotate(0, -10 * Time.deltaTime, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {

            text = "Tutorial";
            if (transform.localScale.x <= .2f)
            {
                transform.localScale += new Vector3(.05f, .05f, .05f);
                audio.Play();
            }

            if (Input.GetMouseButtonDown(0))
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<ButtonAction>()._levelSelect = levelSelect;
                unityEvent.Invoke();
            }
        }
        else
        {
            transform.localScale = storeSize;
            text = "";
        }
    }

    private void OnGUI()
    {
        Rect rect = new Rect(0, Screen.height - 100, Screen.width, 120);
        GUI.Label(rect, text, style);
    }
}
