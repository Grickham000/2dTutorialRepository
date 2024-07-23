using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Creating the variable that will track the player activity, collitions, movement etc.
    private Rigidbody2D body;
    // creating the variable to get animator properties and control its behaviour
    private Animator anim;
    //Creating the object that has the properties for the box collider features.
    private BoxCollider2D boxCollider;

    //Serialized Field added to the speed private variable so that it can be edited from unity
    [SerializeField] private float speed;
    //Serialized Field added to the jumpPower private variable so that it can be edited from unity
    [SerializeField] private float jumpPower;
    //Creating the layer mask to interact with the ground
    [SerializeField] private LayerMask groundLayer;
    //Creating the layer mask to interact with the wall
    [SerializeField] private LayerMask wallLayer;

    private float wallJumpCooldown;
    private float HorizontalInput;

    private bool grounded;

    // Loads every time the game is loading
    private void Awake()
    {
        //do not forget to initialize the objects we will be interacting with, if not the references wont know what do to
        //with the objects that do not have any properties to work with, so imagine you set up the boxCollider, then you dont
        //initialize it, what will happen is that at the moment of using the box collider nothing will be found, even if you have
        //the box colliders configured in unity, nothing happen.

        //Takes the relation from the unity configuration of the game object, if we select the game object player
        //then attach this script, we are getting the component from that game object using the below line
        //remembering that the rigid body component is for the physics of the game object.
        body = GetComponent<Rigidbody2D>();
        //Getting the animator properties of the game object
        anim = GetComponent<Animator>();
        //initializing the BoxCollider
        boxCollider = GetComponent<BoxCollider2D>();
    }

    //loads with every frame of the game to keep track of the activity like a watcher loop
    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        //we check if the horizontal Input is positive or necative compared with the value we are getting to move the player
        //use the transform.localScale to change the scale vector of the player, in which if you compare in unity, and change the x
        //scale to -1 you'll see that the image will flip horizontally
        if (HorizontalInput > 0.01f)
        {
            body.transform.localScale = Vector3.one;
        }
        if(HorizontalInput < -0.01f)
        {
            body.transform.localScale = new Vector3(-1,1,1);
        }

        //Set the parameter form the game object animator component with the values we want
        anim.SetBool("run", HorizontalInput != 0);
        anim.SetBool("grounded",isGrounded());

        if (wallJumpCooldown > 0.2f)
        {
            //below we are intializing a new object with a 2d vector, getting the arrow left and right input to increment the velocity
            //on that direction, then we leave the y velocity(movement) untouched.
            body.velocity = new Vector2(HorizontalInput * speed, body.velocity.y);

            if (onWall())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3;
            }

            //below we are checking if the space button is getting pressed if yes then we are moving in the Y axis with the velocity
            // of speed variable.
            if (Input.GetKey(KeyCode.Space))
            {
                jump();
            }

        }
        else {
            wallJumpCooldown += Time.deltaTime;
        }


    }
    private void jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (HorizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10,0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            
            wallJumpCooldown = 0;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded()
    {
        //Ray cast sents a ray trough the objects intersecting with them, telling you information about the object it intersects with,
        //you can change its lenght and properties, and the BoxCast is the width we want, normaly the width of the player.
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        //if it is on the ground returns null otherwise true.
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

}
