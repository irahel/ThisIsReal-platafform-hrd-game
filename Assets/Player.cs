using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

	[SerializeField] private Animator animControl;
	[SerializeField] private Animator colliderControl;

	//move
	[SerializeField] private float speed;

	//jump
	[SerializeField] public float jumpForce;
	[SerializeField] private Transform floorVerify;
	private int jumpCounter;
	private bool canJump;

	private bool stateSquat;

	[SerializeField] private float maxLife;
	private float Life;

	[SerializeField] private PlayerStatusUI status;

	void Start () 
	{		
		jumpCounter = 0;
		canJump = true;
		stateSquat = false;
		Life = maxLife;
		status.setInitial (Life);
	}
	

	void Update () 
	{
		move ();
		jump ();
		down ();
	}

	void move()
	{	
		float usageSpeed = speed;
		if (stateSquat) 
		{
			usageSpeed = speed / 4;
		}


		float snapSpeed = Input.GetAxisRaw ("Horizontal");
		if (snapSpeed > 0)
		{
			transform.Translate(Vector2.right * usageSpeed * Time.deltaTime);		
			transform.eulerAngles = new Vector2(0, 0);
		}
		else if (snapSpeed < 0)
		{
			transform.Translate(Vector2.right * usageSpeed * Time.deltaTime);		
			transform.eulerAngles = new Vector2(0, 180);
		}

		animControl.SetFloat("Speed", Mathf.Abs(snapSpeed));
	}

	void jump()
	{
		
		canJump = Physics2D.Linecast(transform.position, floorVerify.position, 1 << LayerMask.NameToLayer("Floor"));


		if (canJump) 
		{
			jumpCounter = 0;
			animControl.SetTrigger("endJump");
		}


		if (Input.GetButtonDown("Jump") && jumpCounter < 2)
		{   
			GetComponent<Rigidbody2D>().Sleep();
			GetComponent<Rigidbody2D>().WakeUp();
			GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
			animControl.SetTrigger("Jump");
			jumpCounter++;
		}


	}

	void down()
	{
		stateSquat = Input.GetAxisRaw ("Squat") > 0;
		animControl.SetBool ("Down", stateSquat);
		colliderControl.SetBool ("Down", stateSquat);
	}

	void takeDamage(float damage)
	{
		
		if (Life - damage > 0) 
		{
			Life -= damage;
		}
		else 
		{
			Life = 0;
			//call death
		}
		status.ApllyDamage (damage);


	}

	void gainLife(float gain)
	{
		if (Life + gain > maxLife) 
		{
			Life = maxLife;
		
		}
		else 
		{
			Life += gain;
		}
		status.ApllyDamage (-gain);
	}

	//TEMPORARY TEST
	public void buttonDamageMinus()
	{
		takeDamage (10);
	}
	public void buttonDamagePlus()
	{
		gainLife (10);
	}
}
