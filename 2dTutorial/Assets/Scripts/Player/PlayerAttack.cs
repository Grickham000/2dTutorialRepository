using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    //initializing variables for the Attack script.
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] Fireballs;

    private Animator anim;
    //invoking our PlayerMovement script class to use its properties, this ensures the actions are related.
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        //getting the components from the game object we'll be working with.
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //check if we can attack and are attacking.
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();
        //adds the time since the last frame.
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        //indicating which animation will be activated
        anim.SetTrigger("attack");
        //selects what happen when the attack is triggered.
        cooldownTimer = 0;
        Fireballs[FindFireball()].transform.position = FirePoint.position;
        Fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(MathF.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i = 0; i< Fireballs.Length; i++){
            if (!Fireballs[i].activeInHierarchy) return i;
        }
        return 0;
    }
}