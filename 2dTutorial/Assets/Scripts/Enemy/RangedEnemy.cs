
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Ranged attack parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;

    [Header("Collider parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header("Player layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Audio clips")]
    [SerializeField] AudioClip FireballClip;

    //References

    private Animator anim;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player insight
        // Debug.Log(cooldownTimer);
        if (PlayerInSight())
        {
            if (cooldownTimer > attackCooldown)
            {
                //Debug.Log("entering in sight");
                cooldownTimer = 0;
                anim.SetTrigger("rangedAttack");
            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }

    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        SoundManager.instance.PlaySound(FireballClip);
        fireBalls[findFireball()].transform.position = firePoint.position;
        fireBalls[findFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }

    private int findFireball()
    {
        for(int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        //Debug.Log(hit.collider);

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
