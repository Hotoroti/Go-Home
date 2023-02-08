using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarUpdates : MonoBehaviour
{
    private float baseMaxHP = 2000f;
    private float maxHP;
    private float currentHP;
    private RectTransform healthBar;

    void Start()
    {
        healthBar = GetComponent<RectTransform>();
        SetMaxHP(baseMaxHP);
        SetHP(baseMaxHP);
    }

    void Update()
    {
        //Clamp the hp between 0 and the maximum hp
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
        healthBar.sizeDelta = new Vector2((currentHP/maxHP) * 130, 20);
        //If the base is down you go to the game over screen
        if(currentHP <= 0)
        {
            SceneManager.LoadScene(6);
        }
    }
    //Set the max hp
    internal void SetMaxHP(float amount)
    {
        maxHP = amount;
    }
    //Set the current hp
    internal void SetHP(float amount)
    {
        currentHP = amount;
    }
    //Remove hp/deal damage
    internal void RemoveHP(float amount)
    {
        currentHP -= amount;
    }
    //add hp/heal damage
    internal void AddHP(float amount)
    {
        currentHP += amount;
    }
}
