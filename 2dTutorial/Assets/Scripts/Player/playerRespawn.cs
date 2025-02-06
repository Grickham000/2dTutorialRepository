
using UnityEngine;

public class playerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkPointSound;//Sound when picking new checkpoint
    private Transform currentCheckpoint;//store last checkpoint
    private Health playerHealth;
    private UIManager uiManager;
    private GameObject currentCheckpointRoom;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }
    
    public void CheckRespawn()
    {
        //check if checkpoint is available
        if (currentCheckpoint == null) {
            //show game over screen
            uiManager.GameOver();
            return;
        }

        transform.position=currentCheckpoint.position;//Move player to checkpoint position
        currentCheckpointRoom.GetComponent<Room>().ActivateRoom(true);
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
            currentCheckpointRoom=collision.GetComponent<RoomManager>().getRoom();
            collision.GetComponent<Collider2D>().enabled = false;//deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear");//trigger checkpoint annimation
            
        }
    }
}
