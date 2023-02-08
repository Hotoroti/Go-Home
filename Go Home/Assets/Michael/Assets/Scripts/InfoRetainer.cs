using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//retains per session which levels have been completed to unlock them in the level selection

[CreateAssetMenu(fileName = "Retainer", menuName = "Assets/Michael/Assets/Prefab")]
public class InfoRetainer : ScriptableObject
{
    public int levelCompleted = 0;
}
