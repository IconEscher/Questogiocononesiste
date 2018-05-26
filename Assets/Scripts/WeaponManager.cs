using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public int damage;
	//public float speedWeapon;
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
			Vector2 moveDirection = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
            // Applying velocity to weapon
            rb.AddForce(moveDirection.normalized * 150f);
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
                    gm.weaponCanvas.sprite = this.sprite.sprite;
				this.onEquip = true;
				gm.indexAmmo = 3;
                gm.UpdateAmmo();
					
			}
			break;
			
		}

	}
}


