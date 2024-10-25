
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy animator")]
    [SerializeField] Animator anim;

    private void Awake()
    {
        initScale = transform.localScale;
    }


    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                ChangeDirection();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                ChangeDirection();
            }
        }

    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    private void ChangeDirection()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            movingLeft = !movingLeft;
        }
        
    }


    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);
        //make enemy look to direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,enemy.localScale.y,enemy.localScale.z);

        //make enemy move to direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * speed * _direction, enemy.position.y,enemy.position.z);
    }
}
