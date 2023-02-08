using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator anim;
    internal const string idle = "Idle"; //String for the idle animation
    internal const string walking = "Walking"; //String for the walking animation
    internal const string shooting = "Shooting"; //String for the shooting animation
    internal const string melee = "Melee"; //String for the melee animation
    internal const string dead = "Dead"; //String for the dead animation
    public string enemieCurrentState; //String for wat state it is

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play(idle); //Start gameobject with idle animation
    }

    private void Update()
    {
        //Switch statement
        switch (enemieCurrentState)
        {
            case "Idle": //if the state is idle
                anim.Play(idle); //Play idle animation
                break;
            case "Walking": //if state is walking
                anim.Play(walking); //Playe walking animation
                break;
            case "Shooting": //if state is shooting
                anim.Play(shooting); //Playe shooting animation
                break;
            case "Melee": //if state is melee
                anim.Play(melee); //play melee animation
                break;
            case "Dead": //if state is dead
                anim.Play(dead); //play dead animation
                break;
        }
    }
}
