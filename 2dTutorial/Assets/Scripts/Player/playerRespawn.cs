
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkPointSound;//Sound when picking new checkpoint
    private Transform currentCheckpoint;//store last checkpoint
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }
    
    public void Respawn()
    {
        transform.position=currentCheckpoint.position;//Move player to checkpoint position
        //Restore player health and reset animation
        playerHealth.Respawn();
        //Move camera to the last checkpoint room
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }
    //activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "CheckPoint")
        {
            currentCheckpoint = collision.transform;//store position at the activated checkpoint
            SoundManager.instance.PlaySound(checkPointSound);
            collision.GetComponent<Collider2D>().enabled = false;//deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear");//trigger checkpoint annimation
            
        }
    }
}
