using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour {

	public Image currentHealthBar;
	float hitPoint = 150;
	float maxHitPoint = 150;


void Start()
{
	UpdateHealthBar();
}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			TakeDamage (15);
		}
	}

void UpdateHealthBar()
{
		float ratio = hitPoint / maxHitPoint;
		currentHealthBar.rectTransform.localScale = new Vector3 (1, ratio, 1);
}
		
	void TakeDamage(float damage)
	{
		hitPoint -= damage;
		UpdateHealthBar ();
	}

}
