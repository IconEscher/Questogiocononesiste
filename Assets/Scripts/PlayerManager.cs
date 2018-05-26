using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {


	[Header("Player Settings")]
	public float speed;
	public bool isMoving;
	public float maxHealth = 5;
    public float health = 5;
	public bool invulnerable = false;

	bool movingHor;
	bool movingVer;
	public Vector2 direction;

	float invCount = 3;

    public Image playerHealth;

    // START
    void Start () {
		direction = Vector2.zero;
        playerHealth = GameObject.Find("PlayerHealth").GetComponent<Image>();
	}

	// UPDATE
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
		if (direction == Vector2.zero) 
		{
			movingVer = false;
			movingHor = false;
		}

	}

	private void Move()
	{
		this.gameObject.transform.Translate (direction * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy" && invulnerable == false) 
		{
			StartCoroutine (Invulnerability());
		}

		if (col.tag == "WeaponPoint") {
			if (col.GetComponent<WeaponPoint> ().weaponUp == true) {
				col.GetComponent<WeaponPoint>().weaponUp = false;
			}
		}
	}

	//Removes health and starts invulnerability
	public IEnumerator Invulnerability()
	{
		invulnerable = true;
		health--;
        UpdateHealthBar();
        if (health == 0)
        {
            HasLost();
            yield break;
        }

        invCount = 3;
		while (invCount > 0)
		{
			invCount -= 1;
			CheckInvulnerability ();
			//Debug.Log ("counter: " + invCount + " - " + "bool: " + invulnerable);
			yield return new WaitForSeconds (1);
		}
	}

	// Checks if he's still invulnerable
	void CheckInvulnerability()
	{
		if (invCount <= 0.5f)
			invulnerable = false;
	}

    public void UpdateHealthBar()
    {
        float newHP = health / maxHealth;
        Debug.Log(newHP);
        playerHealth.transform.localScale = new Vector3(playerHealth.transform.localScale.x, newHP, playerHealth.transform.localScale.z);

    }

    public void HasLost()
    {
        PlayerManager pm = GameObject.Find("Player").GetComponent<PlayerManager>();
        pm.invulnerable = true;
        ObjectiveManager om = GameObject.Find("Objectives").GetComponent<ObjectiveManager>();
        om.endGameCanvas.gameObject.SetActive(true);
        om.lossImage.gameObject.SetActive(true);
    }
}
