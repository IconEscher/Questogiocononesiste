using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public PlayerManager pm;

	void OnColliderEnter2D(Collider2D col)
	{
		pm.direction = Vector2.zero;
	}

}
