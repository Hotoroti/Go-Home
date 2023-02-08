using System.Collections;
using UnityEngine;

public class PlayerSwordAttacks : MonoBehaviour
{ 
    private PlayerAnimations playerAnimations;
    private GameObject playerObject;
    private Player player;
    private Animator animator;
    private PlayerHUD playerHUD;
    
    internal bool playerIdle;

    // Normal attack
    internal KeyCode attackKeyCode;
    private float playerAttackDamage;
    private float playerRageAttackDamage;
    private float knockBackForceAttack;
    private float playerAttackCD;
    internal bool playerAttackOnCD;

    // Spin attack
    internal KeyCode spinKeyCode;
    private float playerSpinDamage;
    private float playerRageSpinDamage;
    private float knockBackForceSpin;
    internal float playerSpinDuration;
    internal float playerSpinCD;
    internal bool playerSpinOnCD;

    // ChargeUp
    internal KeyCode chargeKeyCode;
    private float playerRageChargeUpDamage;
    private float knockBackForceChargeUp;
    private float playerChargeUpDuration;
    internal bool playerChargeUpHits;
    internal bool playerChargingUp;

    // Charge attack
    private float playerChargeDamage;
    private float playerRageChargeDamage;
    private float knockBackForceCharge;
    internal float playerChargeSpeed;
    private float playerChargeDuration;
    internal float playerChargeCD;
    internal bool playerChargeOnCD;

    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        animator = playerObject.GetComponent<Animator>();
        playerAnimations = playerObject.GetComponent<PlayerAnimations>();
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<PlayerHUD>();

        playerIdle = true;

        // Normal attack
        attackKeyCode = KeyCode.Mouse0;
        playerAttackDamage = 10;
        playerRageAttackDamage = 15;
        knockBackForceAttack = 50;
        playerAttackCD = 0.5f;
        playerAttackOnCD = false;

        // Spin attack
        spinKeyCode = KeyCode.Mouse1;
        playerSpinDamage = 20;
        playerRageSpinDamage = 25;
        knockBackForceSpin = 70;
        playerSpinDuration = 1;
        playerSpinCD = 3;
        playerSpinOnCD = false;

        // ChargeUp
        chargeKeyCode = KeyCode.Space;
        playerRageChargeUpDamage = 2;
        knockBackForceChargeUp = 10;
        playerChargeUpDuration = 2;
        playerChargeUpHits = false;
        playerChargingUp = false;

        // Charge attack
        playerChargeDamage = 30;
        playerRageChargeDamage = 35;
        playerChargeSpeed = 60;
        knockBackForceCharge = 100;
        playerChargeDuration = 0.25f;
        playerChargeCD = 5;
        playerChargeOnCD = false;

    }

    void Update()
    {
        // Normal attack
        if (Input.GetKeyDown(attackKeyCode) && !playerAttackOnCD && playerIdle)
        {
            StartCoroutine(AttackCD());
            StartCoroutine(playerAnimations.AttackAnimation());
        }

        // Spin attack
        if (Input.GetKeyDown(spinKeyCode) && !playerSpinOnCD && playerIdle)
        {
            StartCoroutine(SpinCD());
            StartCoroutine(playerAnimations.SpinAnimation());
        }

        // ChargeUp
        if (Input.GetKeyDown(chargeKeyCode) && !playerChargingUp && !playerChargeOnCD && playerIdle)
        {
            StartCoroutine(ChargeUp());
            StartCoroutine(playerAnimations.ChargeUpAnimation());
        }
    }

    // Collision between enemy & player attacks
    private void OnTriggerEnter(Collider other)
    {
        // ADD KNOCKBACK
        if (other.CompareTag("Enemy"))
        {
            if (animator.GetBool("PlayerAttack"))
            {
                other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
                enemy.TakeDamage(playerAttackDamage, knockBackForceAttack);
            }

            if (animator.GetBool("PlayerRageAttack"))
            {
                other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
                enemy.TakeDamage(playerRageAttackDamage, knockBackForceAttack);
            }

            if (animator.GetBool("PlayerSpin"))
            {
                other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
                enemy.TakeDamage(playerSpinDamage, knockBackForceSpin);
            }

            if (animator.GetBool("PlayerRageSpin"))
            {
                other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
                enemy.TakeDamage(playerRageSpinDamage, knockBackForceSpin);
            }

            if (animator.GetBool("PlayerCharge"))
            {
                other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
                enemy.TakeDamage(playerChargeDamage, knockBackForceCharge);
            }

            if (animator.GetBool("PlayerRageCharge"))
            {
                other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
                enemy.TakeDamage(playerRageChargeDamage, knockBackForceCharge);
            }
        }        
    }

    // Collision between enemy & player PlayerRageChargeUp attack
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (animator.GetBool("PlayerRageChargeUp") && !playerChargeUpHits)
            {
                other.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
                StartCoroutine(ChargeUpDamige(enemy));
            }
        }
    }

    private IEnumerator AttackCD()
    {
        playerIdle = false;
        playerAttackOnCD = true;
        yield return new WaitForSeconds(playerAttackCD);
        playerAttackOnCD = false;
        playerIdle = true;
    }

    private IEnumerator SpinCD()
    {
        playerIdle = false;
        yield return new WaitForSeconds(playerSpinDuration);
        playerIdle = true;

        StartCoroutine(playerHUD.AbilitySpin(playerSpinCD));

        playerSpinOnCD = true;
        yield return new WaitForSeconds(playerSpinCD);
        playerSpinOnCD = false;
    }

    private IEnumerator ChargeUp()
    {
        float startTime = Time.time;

        playerIdle = false;
        playerChargingUp = true;

        while (Time.time < startTime + playerChargeUpDuration)
        {
            yield return null;
            player.playerShield = 5;
            if (Input.GetKeyDown(KeyCode.Space) && playerChargingUp && !playerChargeOnCD)
            {
                break;
            }
        }

        player.playerShield = 0;
        playerChargingUp = false;
        StartCoroutine(Charge());
    }

    private IEnumerator ChargeUpDamige(Enemy enemy)
    {
        playerChargeUpHits = true;
        enemy.TakeDamage(playerRageChargeUpDamage, knockBackForceChargeUp);
        yield return new WaitForSeconds(0.5f);
        playerChargeUpHits = false;
    }

    private IEnumerator Charge()
    {
        float startTime = Time.time;

        Vector3 direction = playerObject.transform.forward;

        while (Time.time < startTime + playerChargeDuration)
        {
            player.playerController.Move(direction * playerChargeSpeed * Time.deltaTime);

            yield return null;
        }

        playerIdle = true;
        StartCoroutine(ChargeCD());
    }

    private IEnumerator ChargeCD()
    {
        StartCoroutine(playerHUD.AbilityCharge(playerChargeCD));

        playerChargeOnCD = true;
        yield return new WaitForSeconds(playerChargeCD);
        playerChargeOnCD = false;
    }
}
