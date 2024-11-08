using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [Header("SFX")]
    [SerializeField] private AudioClip healthPickSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(healthPickSound);
            collision.GetComponent<Health>().Heal(healthValue);
            gameObject.SetActive(false);
        }
    }

}
