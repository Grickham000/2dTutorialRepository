using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    
    //Firetrap damage configuration
    [Header ("Enemy damage")]
    [SerializeField] private float damage;

    //Define the timers used by the firetrap to be activated
    [Header("FireTrap timers")]
    [SerializeField] private float fireTrapDelay;
    [SerializeField] private float activatedTime;

    private bool triggered;
    private bool active;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Health playerHealth;

    [Header("SFX")]
    [SerializeField] private AudioClip fireSound;

    private void Update()
    {
        if (playerHealth !=null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void Awake()
    {
        //initializing animator and sprint renderer objects
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if(active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        //turn the sprint red when the player activates the trap
        triggered = true;
        spriteRenderer.color = Color.red;

        //wait for delay, active trap, turn on animation, turn back to normal
        yield return new WaitForSeconds(fireTrapDelay);
        SoundManager.instance.PlaySound(fireSound);
        spriteRenderer.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //wait until x seconds, deactivate the trap and reset all variables
        yield return new WaitForSeconds(activatedTime);
        active = false;
        triggered = false;
        anim.SetBool("activated",false);
    }

}
