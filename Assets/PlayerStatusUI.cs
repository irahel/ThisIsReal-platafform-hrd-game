using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour 
{	
	private float atualLife;

	[SerializeField] private Slider slider;
	[SerializeField] private Image progressiveBar;

	[SerializeField] private float flashSpeed;
	private Color flashColour = new Color(1f, 0f, 0f, 1f);
	private Color greenColour = new Color(35f, 167f, 31f, 255f);

	private bool damaged;

	void Start()
	{
		damaged = false;
	}

	public void setInitial(float maxLife)
	{
		slider.maxValue = maxLife;
		atualLife = 0;
	}
	

	void Update () 
	{
		if(damaged)
		{			
			progressiveBar.color = flashColour;
		}
		else
		{
			progressiveBar.color = Color.Lerp (progressiveBar.color, Color.green, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void ApllyDamage(float damage)
	{
		damaged = true;

		atualLife -= damage;

		slider.value = atualLife;
	}
		
}	
