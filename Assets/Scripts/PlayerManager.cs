using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


	[Header("Player Settings")]
	public float speed = 5;
	public bool isMoving;
	[SerializeField]
	bool movingHor;
	[SerializeField]
	bool movingVer;
	public Vector2 direction;




	// Use this for initialization
	void Start () {
		direction = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
		Move ();
	}

	private void GetInput()
	{
		direction = Vector2.zero;
		isMoving = false;
		StartCoroutine (JustWait ());

			if (Input.GetKey (KeyCode.W) && !movingHor) {
				direction += Vector2.up;
				movingVer = true;
				isMoving = true;
			}
			if (Input.GetKey (KeyCode.S) && !movingHor) {
				direction += Vector2.down;
				movingVer = true;
				isMoving = true;
			}
			if (Input.GetKey (KeyCode.A) && !movingVer) {
				direction += Vector2.left;
				movingHor = true;
				isMoving = true;
			}
			if (Input.GetKey (KeyCode.D) && !movingVer) {
				direction += Vector2.right;
				movingHor = true;
				isMoving = true;
			}

		}


	IEnumerator JustWait()
	{
		yield return new WaitForSeconds (0.2f);
		if (direction == Vector2.zero) {
			movingVer = false;
			movingHor = false;
		}

			
	}

	private void Move()
	{
		this.gameObject.transform.Translate (direction * speed * Time.deltaTime);
	}


}
