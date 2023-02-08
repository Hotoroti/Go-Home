using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventTriggers : MonoBehaviour
{
    //This method is called in the attack animation when event is reached
    public void SpawnProjectileAnimationEvent()
    {
        Range_Attack.startSpawningBullet = true;
    }
    //This method is called when the event is finsihed so that all bool returns to default
    public void DissableAllAnimationEvent()
    {
        Range_Attack.startSpawningBullet = false;
    }

    public void PlayAudioAnimationEvent()
    {

    }
}
