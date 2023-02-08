using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnknownPlanetScript : MonoBehaviour
{
    [SerializeField] InfoRetainer retainer;
    private AudioSource audio;
    private Vector3 storeSize;
    public GameObject planet;
    GUIStyle style;
    string text, labelText;
    int lastLevel;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        storeSize = transform.localScale;
        planet = this.gameObject;
        text = "";
        lastLevel = retainer.levelCompleted;
        style = new GUIStyle();
        style.fontSize = 45;
        style.normal.textColor = Color.cyan;
        style.alignment = TextAnchor.MiddleCenter;
        style.font = Resources.Load<Font>("Kenney Future");
    }

    // Update is called once per frame
    void Update()
    {
        TextSwitcher();
        transform.Rotate(0, -10 * Time.deltaTime, 0);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            text = labelText;
            if (transform.localScale.x <= 1f)
            {
                transform.localScale += new Vector3(.1f, .1f, .1f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                audio.Play();
            }

        }
        else
        {
            transform.localScale = storeSize;
            text = "";
        }
    }

    void TextSwitcher()
    {
        switch (lastLevel)
        {
            case 3:
                labelText = "More to come...";
                break;
            default:
                labelText = "???";
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
