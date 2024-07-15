using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Creating the variable that will track the player activity, collitions, movement etc.
    private Rigidbody2D body;
    // creating the variable to get animator properties and control its behaviour
    private Animator anim;

    //Serialized Field added to the speed private variable so that it can be edited from unity
    [SerializeField] private float speed;
    private bool grounded;

    // Loads every time the game is loading
    private void Awake()
    {
        //Takes the relation from the unity configuration of the game object, if we select the game object player
        //then attach this script, we are getting the component from that game object using the below line
        //remembering that the rigid body component is for the physics of the game object.
        body = GetComponent<Rigidbody2D>();
        //Getting the animator properties of the game object
        anim = GetComponent<Animator>();
    }

    //loads with every frame of the game to keep track of the activity like a watcher loop
    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        //below we are intializing a new object with a 2d vector, getting the arrow left and right input to increment the velocity
        //on that direction, then we leave the y velocity(movement) untouched.
        body.velocity = new Vector2(HorizontalInput*speed,body.velocity.y);

        //we check if the horizontal Input is positive or necative compared with the value we are getting to move the player
        //use the transform.localScale to change the scale vector, in which if you compare in unity, and change the x
        //scale to -1 you'll see that the image will flip horizontally
        if (HorizontalInput > 0.01f)
        {
            body.transform.localScale = Vector3.one;
        }
        if(HorizontalInput < -0.01f)
        {
            body.transform.localScale = new Vector3(-1,1,1);
        }

        //below we are checking if the space button is getting pressed if yes then we are moving in the Y axis with the velocity
        // of speed variable.
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            jump();
        }

        //Set the parameter form the game object animator component with the values we want
        anim.SetBool("run", HorizontalInput != 0);
        anim.SetBool("grounded",grounded);


    }
    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded= true;
        }
    }

}
