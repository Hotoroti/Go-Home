using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : MonoBehaviour
{
    [SerializeField] private Text message;
    [SerializeField] private Material material;

    private AudioSource bonk;

    internal int tutorialPart = 0;

    private bool Action1Complete;
    private bool Action2Complete;
    private bool Action3Complete;
    private bool Action4Complete;
    private bool Action5Complete;
    private bool Action6Complete;
    private bool Action7Complete;

    private void Start()
    {
        bonk = GetComponent<AudioSource>();
        material.color = Color.cyan;
    }

    //this is to change the color with an interval so the dummy will be visibly red for a bit
    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(.25f);
        material.color = Color.cyan;
        yield return null;
    }

    private void Update()
    {
        //these if statements are to check if the part of tutorial is completed correctly by using the righ moves
        if (Input.GetKeyDown(KeyCode.Mouse0) && tutorialPart == 0)
        {
            Action1Complete = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && tutorialPart == 1)
        {
            Action2Complete = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && tutorialPart == 2)
        {
            Action3Complete = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && tutorialPart == 3)
        {
            Action4Complete = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && tutorialPart == 4)
        {
            Action5Complete = true;
        }
        if(Input.GetKeyDown(KeyCode.E) && tutorialPart == 5)
        {
            Action6Complete = true;
        }
        if(Input.GetKeyDown(KeyCode.E) && tutorialPart == 6)
        {
            Action7Complete = true;
        }

        // here we change the text for the different messages we want want to display in the tutorial
        if (tutorialPart == 3 && Action4Complete)
        {
            tutorialPart += 1;
            message.text = "Let's test your towers next! Towers will fire at enemies automatically, press E to pick a location for a tower!";
            return;
        }
        if(tutorialPart == 4 && Action5Complete)
        {
            tutorialPart += 1;
            message.text = "Now press E again to place the tower.";
            return;
        }
        if(tutorialPart == 5 && Action6Complete)
        {
            tutorialPart += 1;
            message.text = "Now get close to the tower and press E a third time to put the tower back into your inventory and you're on your way!";
            return;
        }
        if (tutorialPart == 6 && Action7Complete)
        {
            tutorialPart += 1;
            message.text = "Very good! You look ready to go home, press P to go pause and go to the level select screen.";
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        bonk.Play();
        material.color = Color.red;
        StartCoroutine(ChangeColor());
        if (other.gameObject.tag == "Player" && tutorialPart == 0 && Action1Complete)
        {
            tutorialPart += 1;
            message.text = "Great! Now try a spin attack by pressing right click.";
            return;
        }
        if (other.gameObject.tag == "Player" && tutorialPart == 1 && Action2Complete)
        {
            tutorialPart += 1;
            message.text = "Awesome! How about a dashStrike this time by pressing the spacebar.";
            return;
        }
        if (other.gameObject.tag == "Player" && tutorialPart == 2 && Action3Complete)
        {
            tutorialPart += 1;
            message.text = "You're doing Great! Now show me your dashing skills by pressing shift.";
            return;
        }
    }
}
