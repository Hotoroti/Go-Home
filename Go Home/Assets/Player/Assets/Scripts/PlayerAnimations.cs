using System.Collections;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerAudioManager playerAudioManager;

    private float animationTimeAttack;
    private float animationTimeSpin;
    private float animationTimeRageSpin;
    private float animationTimeChargeUp;
    private float animationTimeCharge;
    private float animationTimeRage;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        playerAudioManager = GameObject.Find("PlayerAudioManager").GetComponent<PlayerAudioManager>();

        animationTimeRage = 10f;
        GetAnimationLengths();
    }

    internal IEnumerator RageAnimation()
    {
        animator.SetBool("PlayerRage", true);
        playerAudioManager.Play("Raging");
        yield return new WaitForSeconds(animationTimeRage);
        playerAudioManager.Stop("Raging");
        animator.SetBool("PlayerRage", false);
    }

    internal IEnumerator AttackAnimation()
    {
        if (animator.GetBool("PlayerRage"))
        {
            animator.SetBool("PlayerRageAttack", true);
            playerAudioManager.Play("RageAttacking");
        }
        else
        {
            animator.SetBool("PlayerAttack", true);
            playerAudioManager.Play("Attacking");
        }

        yield return new WaitForSeconds(animationTimeAttack);

        animator.SetBool("PlayerRageAttack", false);
        animator.SetBool("PlayerAttack", false);
    }

    internal IEnumerator SpinAnimation()
    {
        if (animator.GetBool("PlayerRage"))
        {
            animator.SetBool("PlayerRageSpin", true);
            playerAudioManager.Play("Spinning");
            yield return new WaitForSeconds(animationTimeRageSpin);
        }
        else
        {
            animator.SetBool("PlayerSpin", true);
            playerAudioManager.Play("Spinning");
            yield return new WaitForSeconds(animationTimeSpin);
        }

        playerAudioManager.Stop("Spinning");

        animator.SetBool("PlayerRageSpin", false);
        animator.SetBool("PlayerSpin", false);
    }

    internal IEnumerator ChargeUpAnimation()
    {
        if (animator.GetBool("PlayerRage"))
        {
            animator.SetBool("PlayerRageChargeUp", true);
            playerAudioManager.Play("RageChargingUp");
        }
        else
        {
            animator.SetBool("PlayerChargeUp", true);
            playerAudioManager.Play("ChargingUp");
        }
        for (float t = 0f; t < animationTimeChargeUp; t += Time.deltaTime)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }
        }

        playerAudioManager.Stop("RageChargingUp");
        playerAudioManager.Stop("ChargingUp");

        animator.SetBool("PlayerRageChargeUp", false);
        animator.SetBool("PlayerChargeUp", false);

        StartCoroutine(ChargeAnimation());
    }

    protected IEnumerator ChargeAnimation()
    {
        if (animator.GetCurrentAnimatorClipInfo(1)[0].clip.name == "PlayerRageChargeUp")
            animator.SetBool("PlayerRageCharge", true);
        else
            animator.SetBool("PlayerCharge", true);

        playerAudioManager.Play("Charging");

        yield return new WaitForSeconds(animationTimeCharge);

        playerAudioManager.Stop("Charging");

        animator.SetBool("PlayerRageCharge", false);
        animator.SetBool("PlayerCharge", false);
    }

    // Gets the duration of all attack animations
    public void GetAnimationLengths()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "PlayerAttack":
                    animationTimeAttack = clip.length;
                    break;
                case "PlayerSpin":
                    animationTimeSpin = clip.length;
                    break;
                case "PlayerChargeUp":
                    animationTimeChargeUp = clip.length;
                    break;
                case "PlayerCharge":
                    animationTimeCharge = clip.length;
                    break;
                case "PlayerRageSpin":
                    animationTimeRageSpin = clip.length;
                    break;
            }
        }
    }
}
