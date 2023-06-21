using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //Untouchable variables containing animations and the Phsyics body of the Heart
    private Rigidbody2D rb2d;
    private Animator myAnimator;
    private SpriteRenderer mySprite;
    private bool facingRight = false;
    private bool grounded;
	private bool jumping = false;
    private float hMovement;


    //variables to play with within the editor that control fluidity of movement
    [Header("Public Vars")]
    public float speed;
    public float jumpForce;
    public int heartRate;
    public int graceTime;
    public int blockage;
    public int beatFUCounter;

    [Header("Gizmo Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float radOCircle;

    private void Start()
    {
        //Define the game objects attatched to the heart
        rb2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        //check if input for Left or Right, checking once per frame.
        hMovement = Input.GetAxisRaw("Horizontal");
        myAnimator.SetFloat("speed", Mathf.Abs(hMovement));

        //checks grounded deactivating either the jump or falling animations.
        if (grounded)
        {
            myAnimator.SetTrigger("land");
            myAnimator.SetBool("falling", false);
            myAnimator.ResetTrigger("jump");
        }
        else
        {
            myAnimator.ResetTrigger("land");
        }

        //checks for grounded or on beat jumps.
		if (Input.GetButtonDown("Jump") && ((beatFUCounter < graceTime/2 || beatFUCounter>(3000/heartRate)-graceTime*2)&&(blockage<=0) || grounded))
		{
			jumping = true;
            myAnimator.SetTrigger("jump");
            myAnimator.SetBool("falling", true);
		}

        //sees when jump is let go and switches to the middle air time animation.
        if((Input.GetButtonUp("Jump") && !grounded)||rb2d.velocity.y<-0.01)
        {
            myAnimator.SetBool("falling", true);
            myAnimator.ResetTrigger("jump");
        }

        //on a failed press locks out the jump key press.
        if (Input.GetButtonDown("Jump") && !(beatFUCounter < graceTime || beatFUCounter > (3000 / heartRate) - graceTime))
        {
            blockage = 1+graceTime*2;
        }
    }

    private void FixedUpdate()
    {
        //move the character using the speed variable, flips them if they're facing the wrong way.
        rb2d.velocity = new Vector2(hMovement * speed, rb2d.velocity.y);
        Flip(hMovement);
        myAnimator.SetFloat("speed", Mathf.Abs(hMovement));

        //checks gizmo overlap, allowing for jumps when on the ground.
        grounded = Physics2D.OverlapCircle(groundCheck.position, radOCircle, whatIsGround);

        //if jump has been pressed then the heart jumps
        if (jumping)
        {
            jumping = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        //counts down deltatime before jump key can be pressed again.
        if (blockage >= 0) { blockage -= 1; }

        //counts down fifty times per second. If the number accounts for the set heart rate the heart will bounce.
        beatFUCounter -= 1;
        myAnimator.SetFloat("beatCount", beatFUCounter);
        if (beatFUCounter <= 0)
        {
            //bumps the heart once per beat.
            /*rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + 0.7f);*/
            beatFUCounter = (3000 / heartRate);
            
        }

        //Checks grounded and swaps to air animations if not.
        HandleLayers();
    }

    //draws the grounded check gizmo.
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOCircle);
    }

    //function to flip the heart based on input.
    private void Flip(float horizontal)
    {
        if(facingRight && horizontal < 0 || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    //Checks grounded and swaps to air animations if not.
    private void HandleLayers()
    {
        if(!grounded)
        {
            myAnimator.SetLayerWeight(1,1);
        }
        else
        {
            myAnimator.SetLayerWeight(0,1);
        }
    }
}