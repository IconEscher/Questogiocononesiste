using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

	[Header("Prefabs")]
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public GameObject weaponPrefab;
	public GameObject chosenWeapon;

	public GameObject playerStart;

	[Header("Weapons")]
	public List<Sprite> weaponSprites;
	public int chosenSprite;
	public int indexWeapons;

	[Header("Enemies")]
	public List<GameObject> enemyList;
	public List<GameObject> patrolPoints;
	public int numberOfPatrols;
	public int numberOfEnemies;
	//public int indexEnemies;

	private float time;
	public float minTime;
	public float maxTime;
	private float spawnTime;


	// START
	void Start () {
		patrolPoints = new List<GameObject> (numberOfPatrols);
		enemyList = new List<GameObject> (numberOfEnemies);

		for (int i = 0; i < patrolPoints.Capacity; i++) 
		{
			patrolPoints.Add (GameObject.Find ("patrolPoint(" + (i + 1) + ")"));
		}

		GameObject playerGO = Instantiate (playerPrefab, playerStart.transform.position, Quaternion.identity);
		playerGO.name = "Player";

		for (int i = 0; i < numberOfEnemies; i++) 
		{
			GameObject enemyGO = Instantiate (enemyPrefab, patrolPoints [Random.Range (0, patrolPoints.Capacity)].transform.position, Quaternion.identity);
			enemyList.Add (enemyGO);
			enemyGO.name = "Enemy_" + (i + 1);
			//indexEnemies++;
		}


	}
	
	// UPDATE
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0))
			{
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			GameObject weaponGO = Instantiate (chosenWeapon, player.transform.position, Quaternion.identity);
			//chosenSprite = Random.Range (0, weaponSprites.Capacity - 1);
			//weaponGO.GetComponent<SpriteRenderer> ().sprite = weaponSprites [chosenSprite];
			}


	}

	void FixedUpdate() {
		time += Time.deltaTime;

		if (time >= spawnTime && indexWeapons < 4) {
			SpawnWeapon();
			SetRandomTime();
		}
	}
		

	public IEnumerator RespawnEnemies()
	{
		print ("Respawning enemies");
			//indexEnemies = 6;
			//new WaitForSeconds (Random.Range (3, 6));
			GameObject enemyGO = Instantiate (enemyPrefab, patrolPoints [Random.Range (0, patrolPoints.Capacity)].transform.position, Quaternion.identity);
			enemyGO.name = "New_Enemy";
			enemyList.Add (enemyGO);
			yield return null;
		}

	void SpawnWeapon(){
		time = 0;
		GameObject weaponGO = Instantiate (weaponPrefab,  patrolPoints [Random.Range (0, patrolPoints.Capacity)].transform.position, Quaternion.identity);
		chosenSprite = Random.Range (0, weaponSprites.Capacity - 1);
		weaponGO.GetComponent<SpriteRenderer> ().sprite = weaponSprites [chosenSprite];
	}

	void SetRandomTime()
	{
		spawnTime = Random.Range(minTime, maxTime);
	}

}



