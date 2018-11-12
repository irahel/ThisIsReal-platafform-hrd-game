using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

	[SerializeField] private Animator anim_control;

	//move
	[SerializeField] private float speed;

	//jump
	[SerializeField] public float jumpForce;
	[SerializeField] private Transform floorVerify;
	private int jumpCounter;
	private bool canJump;

	void Start () 
	{
		anim_control = GetComponentInChildren<Animator> ();
		jumpCounter = 0;
		canJump = true;
	}
	

	void Update () 
	{
		move ();
		jump ();
	}

	void move()
	{		
		float snapSpeed = Input.GetAxisRaw ("Horizontal");
		if (snapSpeed > 0)
		{
			transform.Translate(Vector2.right * speed * Time.deltaTime);		
			transform.eulerAngles = new Vector2(0, 0);
		}
		else if (snapSpeed < 0)
		{
			transform.Translate(Vector2.right * speed * Time.deltaTime);		
			transform.eulerAngles = new Vector2(0, 180);
		}

		anim_control.SetFloat("Speed", Mathf.Abs(snapSpeed));
	}

	void jump()
	{
		
		canJump = Physics2D.Linecast(transform.position, floorVerify.position, 1 << LayerMask.NameToLayer("Floor"));


		if (canJump) 
		{
			jumpCounter = 0;
			anim_control.SetTrigger("endJump");
		}


		if (Input.GetButtonDown("Jump") && jumpCounter < 2)
		{   
			GetComponent<Rigidbody2D>().Sleep();
			GetComponent<Rigidbody2D>().WakeUp();
			GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
			anim_control.SetTrigger("Jump");
			jumpCounter++;
		}


	}
}
