using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //is the character on the ground or in a jump?
	private bool isGrounded = false;
	//reference to the object's Transform component
	//to determine ground contact
	public Transform groundCheck;
	//ground contact radius
	private float groundRadius = 0.2f;
	//reference to the layer representing the ground
	public LayerMask whatIsGround;
    //variable to set max. character speed
    public float maxSpeed = 10f; 
    //variable to determine the direction of the character to the right / left
    private bool isFacingRight = true;
    //link to animation component
    private Animator anim;

    //Initialization
	private void Start()
    {
        anim = GetComponent<Animator>();
    }
	
    //We perform actions in the FixedUpdate method, because in the Animator component of the character
    //Animate Physics = true is set and the animation is synchronized with physics calculations
	private void FixedUpdate()
    {
        //determine if the character is on the ground
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); 
		//set the corresponding variable in the animator
		anim.SetBool ("Ground", isGrounded);
		//set in the animator the value of the takeoff / fall speed
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        //if the character is jumping - exit the method so that actions related to running are not performed
        // if (!isGrounded)
        //     return;

        //use Input.GetAxis for the X axis. The method returns the value of the axis in the range from -1 to 1.
        //with standard project settings
        //-1 is returned when you press the left arrow on the keyboard (or the A key),
        //1 is returned when you press the right arrow on your keyboard (or the D key)
        float move = Input.GetAxis("Horizontal");

        //in the animation component, change the value of the Speed parameter to the value of the X axis.
        //while we need the value modulus
        anim.SetFloat("Speed", Mathf.Abs(move));

        //we refer to the RigidBody2D character component. set the speed along the X axis,
        //equal to the value of the X-axis multiplied by the value of the max. speed
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        //if you pressed the key to move right, and the character is directed to the left
        if(move > 0 && !isFacingRight)
            //flip the character to the right
            Flip();
        //reverse situation. flip the character to the left
        else if (move < 0 && isFacingRight)
            Flip();
    }

    //A method for changing the direction of a character's movement and mirroring it
    private void Flip()
    {
        //change the direction of the character
        isFacingRight = !isFacingRight;
        //get character size
        Vector3 theScale = transform.localScale;
        //mirror the character along the x-axis
        theScale.x *= -1;
        //set a new character size equal to the old one, but mirrored
        transform.localScale = theScale;
    }

    private void Update()
	{
		//if the character is on the ground and the space bar is pressed...
		if (isGrounded && Input.GetKeyDown (KeyCode.Space)) 
		{
			//set variable in animator to false
			anim.SetBool("Ground", false);
			//apply force upwards to make the character jump
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3000));		
		}
	}
}
