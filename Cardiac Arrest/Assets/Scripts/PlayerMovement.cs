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
    private int beatFUCounter;
    private bool grounded;
	private bool jumping = false;


    //variables to play with within the editor that control fluidity of movement
    [Header("Public Vars")]
    public float speed = 2.0f;
    public float hMovement;
    public float jumpForce;

    public int heartRate;

    [Header("Private Vars")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float radOCircle;

    private void Start()
    {
        //Define the game objects attatched to the heart
        rb2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

    }


    private void Update()
    {
        //check if input for Left or Right, checking once per frame.
        hMovement = Input.GetAxisRaw("Horizontal");
		if (Input.GetButtonDown("Jump") && (Mathf.Abs(rb2d.velocity.y) <= 0.5f) || beatFUCounter < 5) /*&& grounded*/)
		{
			jumping = true;
		}
    }

    private void FixedUpdate()
    {
        //move the character using the speed variable.
        rb2d.velocity = new Vector2(hMovement * speed, rb2d.velocity.y);
        myAnimator.SetFloat("speed", Mathf.Abs(hMovement));
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.001f);
        if (jumping)
        {
			jumping = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
        //counts up fifty times per second. If the number accounts for the set heart rate the heart will bounce.
        beatFUCounter -= 1;
        if (beatFUCounter <= 0)
        {
            //bumps the heart once per beat.
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + 0.5f);
            beatFUCounter = (3000 / heartRate);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, radOCircle);
    }
}