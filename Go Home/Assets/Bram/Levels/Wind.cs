using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float windForce;
    [SerializeField] private float windInterval;
    [SerializeField] private float windDuration;
    [SerializeField] private Player player;
    public ParticleSystem snow;
    private float windTime;

    private bool windActive = true;

    private void OnTriggerStay(Collider other)
    {
        //if the winfd time is higher than the duration stop the wind
        if (windTime >= windDuration)
        {
            //check if the other object is the player
            if (other.gameObject.tag == "Player")
            {
                //here we stop the snow particle effect and the pushing of the player
                snow.emissionRate = 0;
                windActive = false;
                //if the windtime is higher than both the duration and the interval start again and reset the timer
                if (windTime >= windDuration + windInterval)
                {
                    //here we put the snow emision rate to its desired amount while the wind is active
                    snow.emissionRate = 1000;
                    windActive = true;

                    windTime = 0;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        //keep counting up the time the windzone is active
        windTime++;
        //While the windzone is active use this function from the player class to push him back
        if (windActive)
        {
            player.WindForce(new Vector3(windForce, 0, 0));
        }
    }
}

