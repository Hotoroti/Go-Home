using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Animator animator;
    private PlayerAnimations playerAnimations;
    private PlayerAudioManager playerAudioManager;
    private PlayerHUD playerHUD;
    [SerializeField] private GameObject dashExplosionPrefeb;

    // Movement
    internal CharacterController playerController;
    internal Vector3 playerVelocity;
    internal Vector3 playerMovement;
    private float playerSpeed;
    private float gravityValue;
    private bool isRunnig;

    // Cam
    private Ray mouseRay;
    private float midPoint;
    internal Vector3 lookAt;

    // Stats
    [SerializeField] private Image playerHpBar;
    [SerializeField] private Canvas playerHpBarCanvas;
    private const float PLAYER_MAX_HP = 200;
    internal float playerHp;
    internal float playerShield;
    private const float PLAYER_MAX_RAGE = 100;
    internal float playerRageAmount;
    internal float playerRageDuration;
    internal bool playerRage;
    private bool invincibility;
    private float invincibilityDuration;
    private bool knockBack;
    private float knockBackDuration;

    // Dash
    internal KeyCode dashKeyCode;
    private float playerDashSpeed;
    internal float playerDashCD;
    internal float playerDashDuration;
    internal bool playerDashOnCD;

    // Grounded
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    private float groundDistance = 0.4f;
    private bool isGrounded;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        playerController = gameObject.GetComponent<CharacterController>();
        playerAnimations = gameObject.GetComponent<PlayerAnimations>();
        playerAudioManager = GameObject.Find("PlayerAudioManager").GetComponent<PlayerAudioManager>();
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<PlayerHUD>();

        // Movement
        playerSpeed = 12.0f;
        gravityValue = -19.62f;
        isRunnig = false;

        // Stats
        playerHp = PLAYER_MAX_HP;
        playerShield = 0;

        playerRage = false;
        playerRageAmount = 0f;
        playerRageDuration = 10f;

        invincibility = false;
        invincibilityDuration = 1f;

        knockBack = false;
        knockBackDuration = 0.25f;

        // Dash
        playerDashOnCD = false;
        playerDashSpeed = 40;
        playerDashDuration = 0.25f;
        playerDashCD = 2;
        dashKeyCode = KeyCode.LeftShift;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Movement
        playerMovement = new Vector3(Input.GetAxis("Horizontal"), playerMovement.y, Input.GetAxis("Vertical"));
        playerMovement = Vector3.ClampMagnitude(playerMovement, 1f);

        if (!animator.GetBool("PlayerChargeUp") && !animator.GetBool("PlayerRageChargeUp") && !animator.GetBool("PlayerCharge") && !animator.GetBool("PlayerRageCharge") && !knockBack)
            playerController.Move(playerMovement * playerSpeed * Time.deltaTime);

        if (playerMovement != Vector3.zero)
        {
            if (!isRunnig)
            {
                animator.SetBool("PlayerRun", true);
                playerAudioManager.Play("Running");
                isRunnig = true;
            }
        }
        else
        {
            animator.SetBool("PlayerRun", false);
            playerAudioManager.Stop("Running");
            isRunnig = false;
        }

        // Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        playerController.Move(playerVelocity * Time.deltaTime);

        // Cam
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        midPoint = (transform.position - Camera.main.transform.position).magnitude;

        // Make player look at mous and not rotate on y axis
        lookAt = mouseRay.origin + mouseRay.direction * midPoint;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);

        // HP BAR
        playerHpBar.fillAmount = playerHp / PLAYER_MAX_HP;
        playerHpBarCanvas.transform.LookAt(Camera.main.transform);

        // Dash
        if (Input.GetKeyDown(dashKeyCode) && !playerDashOnCD)
        {
            playerAudioManager.Play("Dashing");
            StartCoroutine(Dash());
        }

        // Rage
        if (playerRageAmount >= PLAYER_MAX_RAGE)
        {
            playerRageAmount = PLAYER_MAX_RAGE;
            StartCoroutine(Rage());
        }

        // Player Death
        if (playerHp <= 0)
        {
			SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator Dash()
    {
        float startTime = Time.time;

        Vector3 direction = playerMovement.normalized;

        if (playerRage)
        {
            Instantiate(dashExplosionPrefeb, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            playerAudioManager.Play("RageDashFire");
        }

        while (Time.time < startTime + playerDashDuration)
        {
            playerController.Move(direction * playerDashSpeed * Time.deltaTime);

            yield return null;
        }

        StartCoroutine(DashCD());
    }

    private IEnumerator DashCD()
    {
        StartCoroutine(playerHUD.AbilityDash(playerDashCD));

        playerDashOnCD = true;
        yield return new WaitForSeconds(playerDashCD);
        playerDashOnCD = false;
    }

    public void TakeDamage(float damage, float knockbackForce)
    {
        if (!invincibility)
        {
            playerHp -= (damage - playerShield);

            if (!playerRage)
                playerRageAmount += 50;

            StartCoroutine(KnockBack(knockbackForce));
            StartCoroutine(InvincibilityFrames());
        }
    }

    public void WindForce(Vector3 Force)
    {
        playerController.Move(Force* Time.deltaTime);
    }

    private IEnumerator KnockBack(float knockbackForce)
    {
        knockBack = true;

        float startTime = Time.time;
        Vector3 direction = -playerMovement.normalized;

        while (Time.time < startTime + knockBackDuration)
        {
            playerController.Move(direction * knockbackForce * Time.deltaTime);

            yield return null;
        }

        knockBack = false;
    }

    private IEnumerator InvincibilityFrames()
    {
        invincibility = true;
        yield return new WaitForSeconds(invincibilityDuration);
        invincibility = false;
    }

    // Manage Player Rage
    private IEnumerator Rage()
    {
        playerRage = true;
        StartCoroutine(playerAnimations.RageAnimation());

        float startTime = Time.time;

        while (Time.time < startTime + playerRageDuration)
        {
            playerRageAmount -= PLAYER_MAX_RAGE / playerRageDuration * Time.deltaTime;
            yield return null;
        }

        playerRageAmount = 0;
        playerRage = false;
    }
}
