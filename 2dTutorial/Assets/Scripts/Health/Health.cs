
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Iframes")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    [Header("Components")]
    [SerializeField] Behaviour[] components;

    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private bool invulnerable;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                dead = true;
                
                SoundManager.instance.PlaySound(deathSound);
                //if(GetComponent<PlayerMovement>() != null)
                //GetComponent<PlayerMovement>().enabled = false;
                
                //if(GetComponentInParent<EnemyPatrol>() != null)
                //GetComponentInParent<EnemyPatrol>().enabled = false;

                //if (GetComponentInParent<MeleeEnemy>() != null)
                //    GetComponentInParent<MeleeEnemy>().enabled = false;

                //Disable all attached components.
                foreach ( Behaviour component in components)
                {
                    component.enabled = false;
                }
                anim.SetBool("grounded",true);
                anim.SetTrigger("die");
            }
        }
    }

    public void Heal(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        Heal(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(Invulnerability());
        //Activate All attached component classess
        foreach( Behaviour component in components)
            component.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
    //system collections
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10,11,true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0,0.5f);
            // 2/3 0.66 seconds to make up the 2 seconds of duration *2 for the 2 delays
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes *2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes*2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable=false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
