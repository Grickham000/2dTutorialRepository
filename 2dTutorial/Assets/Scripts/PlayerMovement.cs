using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Creating the variable that will track the player activity, collitions, movement etc.
    private Rigidbody2D body;

    //Serialized Field added to the speed private variable so that it can be edited from unity
    [SerializeField] private float speed;

    // Loads every time the game is loading
    private void Awake()
    {
        //Takes the relation from the unity configuration of the game object, if we select the game object player
        //then attach this script, we are getting the component from that game object using the below line
        //remembering that the rigid body component is for the physics of the game object.
        body = GetComponent<Rigidbody2D>();
    }

    //loads with every frame of the game to keep track of the activity like a watcher loop
    private void Update()
    {
        //below we are intializing a new object with a 2d vector, getting the arrow left and right input to increment the velocity
        //on that direction, then we leave the y velocity(movement) untouched.
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed,body.velocity.y);

        //below we are checking if the space button is getting pressed if yes then we are moving in the Y axis with the velocity
        // of speed variable.
        if (Input.GetKey(KeyCode.Space))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }
    }

}
