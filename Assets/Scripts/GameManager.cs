using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {


	[Header("Prefabs")]
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public GameObject weaponPrefab;
	public GameObject chosenWeapon;

	public GameObject playerStart;

	[Header("Weapons")]
	public List<Sprite> weaponSprites;
	public List<GameObject> weaponPoints;
	public int numberOfWeaponPoints;
	public int chosenSprite;
	//public int indexWeapons;
	public int indexAmmo;
    public Text indexAmmoText;
    public Image weaponCanvas;
    public Sprite transparent;

    [Header("Enemies")]
	public List<GameObject> enemyList;
	public List<Sprite> enemySprites;
	public List<GameObject> patrolPoints;
	public int numberOfPatrols;
	public int numberOfEnemies;
	//public int indexEnemies;

	[Header("Times")]
	public float minTime;
	public float maxTime;
	private float spawnTime;
	private float time;



    // START
    void Start () {
		patrolPoints = new List<GameObject> (numberOfPatrols);
		weaponPoints = new List<GameObject> (numberOfWeaponPoints);
		enemyList = new List<GameObject> (numberOfEnemies);

		for (int i = 0; i < patrolPoints.Capacity; i++) 
		{
			patrolPoints.Add (GameObject.Find ("patrolPoint(" + (i + 1) + ")"));
		}

		for (int i = 0; i < weaponPoints.Capacity; i++)
		{
			weaponPoints.Add (GameObject.Find ("WeaponPoint(" + (i + 1) + ")"));
		}

		GameObject playerGO = Instantiate (playerPrefab, playerStart.transform.position, Quaternion.identity);
		playerGO.name = "Player";

		for (int i = 0; i < numberOfEnemies; i++) 
		{
			GameObject enemyGO = Instantiate (enemyPrefab, patrolPoints [Random.Range (0, patrolPoints.Capacity)].transform.position, Quaternion.identity);
			int randomSkin = Random.Range (0, enemySprites.Capacity - 1);
			enemyGO.GetComponentInChildren<SpriteRenderer> ().sprite = enemySprites [randomSkin];
			enemyList.Add (enemyGO);
			enemyGO.name = "Enemy_" + (i + 1);
			//indexEnemies++;
		}


	}
	
	// UPDATE
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0) && indexAmmo > 0)
			{
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			GameObject weaponGO = Instantiate (chosenWeapon, player.transform.position, Quaternion.identity);
			indexAmmo--;
            UpdateAmmo();
			//chosenSprite = Random.Range (0, weaponSprites.Capacity - 1);
			//weaponGO.GetComponent<SpriteRenderer> ().sprite = weaponSprites [chosenSprite];
			}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

	}

	void FixedUpdate() {
		time += Time.deltaTime;

		if (time >= spawnTime) {
			SpawnWeapon();
			SetRandomTime();
		}
	}
		

	public IEnumerator RespawnEnemies()
	{
			print ("Respawning enemies");
			GameObject enemyGO = Instantiate (enemyPrefab, patrolPoints [Random.Range (0, patrolPoints.Capacity)].transform.position, Quaternion.identity);
			enemyGO.name = "New_Enemy";
			int randomSkin = Random.Range (0, enemySprites.Capacity - 1);
			enemyGO.GetComponentInChildren<SpriteRenderer> ().sprite = enemySprites [randomSkin];
			enemyList.Add (enemyGO);
			yield return null;
		}

	void SpawnWeapon(){
		time = 0;
		int weaponSpawn = Random.Range (0, weaponPoints.Capacity - 1);
		if (!weaponPoints [weaponSpawn].GetComponent<WeaponPoint> ().weaponUp) {
			GameObject weaponGO = Instantiate (weaponPrefab, weaponPoints [weaponSpawn].transform.position, Quaternion.identity);
			chosenSprite = Random.Range (0, weaponSprites.Capacity - 1);
			weaponGO.name = "New_Weapon";
			weaponGO.GetComponent<SpriteRenderer> ().sprite = weaponSprites [chosenSprite];
			weaponPoints [weaponSpawn].GetComponent<WeaponPoint> ().weaponUp = true;
		}
	}

	void SetRandomTime()
	{
		spawnTime = Random.Range(minTime, maxTime);
	}

    public void UpdateAmmo()
    {
        indexAmmoText.text = indexAmmo.ToString();
        if (indexAmmo == 0) weaponCanvas.sprite = transparent;
    }

    public void BackToMenu(string scene)
    {

    }
    
}



