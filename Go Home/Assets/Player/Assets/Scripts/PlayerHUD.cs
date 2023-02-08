using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private GameObject playerObject;
    private Player player;

    // Abilities
    [SerializeField] private Image abilityDash;
    [SerializeField] private Image abilitySpin;
    [SerializeField] private Image abilityCharge;
    
    // Rage
    [SerializeField] private Image playerRage;

    void Start()
    {
		playerObject = GameObject.Find("Player");
		player = playerObject.GetComponent<Player>();

		// Abilities
		abilityDash.fillAmount = 0;
        abilitySpin.fillAmount = 0;
        abilityCharge.fillAmount = 0;

        // Rage
        playerRage.fillAmount = 0;
    }

    void Update()
    {
		Rage();
	}

    // Updates the dash icon in the player HUD
    internal IEnumerator AbilityDash(float playerDashCD)
    {
        float startTime = Time.time;

        abilityDash.fillAmount = 1;

        while (Time.time < startTime + playerDashCD)
        {
            abilityDash.fillAmount -= 1 / playerDashCD * Time.deltaTime;

            if (abilityDash.fillAmount <= 0)
            {
                abilityDash.fillAmount = 0;
            }

            yield return null;
        }
    }

    // Updates the spin icon in the player HUD
    internal IEnumerator AbilitySpin(float playerSpinCD)
    {
        float startTime = Time.time;

        abilitySpin.fillAmount = 1;

        while (Time.time < startTime + playerSpinCD)
        {
            abilitySpin.fillAmount -= 1 / playerSpinCD * Time.deltaTime;

            if (abilitySpin.fillAmount <= 0)
            {
                abilitySpin.fillAmount = 0;
            }

            yield return null;
        }
    }

    // Updates the charge icon in the player HUD
    internal IEnumerator AbilityCharge(float playerChargeCD)
    {
        float startTime = Time.time;

        abilityCharge.fillAmount = 1;
        
        while (Time.time < startTime + playerChargeCD)
        {
            abilityCharge.fillAmount -= 1 / playerChargeCD * Time.deltaTime;

            if (abilityCharge.fillAmount <= 0)
            {
                abilityCharge.fillAmount = 0;
            }

            yield return null;
        }
    }

    // Updates the rage bar in the player HUD
    private void Rage()
    {
        if (player.playerRageAmount <= 100)
        {
            float rage = player.playerRageAmount / 100;

            playerRage.fillAmount = rage;
        }
    }
}
