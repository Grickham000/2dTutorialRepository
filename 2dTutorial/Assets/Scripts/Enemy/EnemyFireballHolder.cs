
using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{

    [SerializeField] private Transform enemy;
    void Update()
    {
        transform.localScale = enemy.localScale;
        
    }
}
