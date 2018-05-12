using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class WeaponManager : MonoBehaviour {

	public int damage;
	public int velocity;
	SpriteRenderer sprite;
	Rigidbody2D rb;
	GameObject weapon;
	GameManager gm;
	public bool onEquip = false;

	// START
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();
		weapon = this.gameObject;

		gm = GameObject.Find("GameManager").GetComponent<GameManager> ();
		//sprite.sprite = gm.weaponSprites [gm.chosenSprite];

		if (onEquip) {
			
			// Saving mouse position
			Vector2 moveDirection = (Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position).normalized * velocity;
			// Applying velocity to weapon
			rb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
		}
	}

	// Colliding with other objects - If they're not in the switch check, it will just ignore it
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log ("collido con: " + col.gameObject.name + " Tag: " + col.tag);

			switch (col.tag) {

		case "Enemy":
			if (onEquip) {
				col.gameObject.GetComponent<EnemyAIManager> ().healthEnemy -= damage;
				col.gameObject.GetComponent<EnemyAIManager> ().CheckHealth ();
				Destroy (weapon);
			}
				break;

		case "Obstacle":
			if (onEquip) {
				Destroy (weapon);
			}
				break;

			case "Player":
				if (!onEquip) {
					Destroy (weapon);
					gm.chosenWeapon.GetComponent<SpriteRenderer> ().sprite = this.sprite.sprite;
				this.onEquip = true;
					
			}
			break;
			
		}

	}
}


