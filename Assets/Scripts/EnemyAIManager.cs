using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


	public class EnemyAIManager : MonoBehaviour {

	public int numberOfPoints = 0;
	public List<GameObject> patrolPoints;
	//public GameObject player;
	int pointsIndex;
	AIDestinationSetter dest;
	public bool isEnemyMoving = false;
	[SerializeField]
	public int healthEnemy = 3;
    GameObject enemy;
	GameManager gm;

	// Use this for initialization
	void Start () {
		patrolPoints = new List<GameObject>(numberOfPoints);
		dest = this.GetComponent<AIDestinationSetter> ();
		enemy = this.gameObject;
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
			
			// Adding patrol points to our list
			for (int i = 0; i < patrolPoints.Capacity; i++) 
			{
				patrolPoints.Add(GameObject.Find("patrolPoint(" + (i + 1) + ")"));
			}
			SelectPoint ();
	}
	
	// Update is called once per frame
	void Update () {
			
			// Hits a raycast - used for finding what it faces
			RaycastHit2D ray = Physics2D.Raycast (transform.position, transform.up, 10f);
			Debug.DrawRay (transform.position, transform.up * 10f, Color.white);

			// Stops patrolling and starts chasing player
			if (ray.collider.name == "Player") 
			{
				dest.target = GameObject.FindGameObjectWithTag ("Player").transform;
			}

			// Selects a new patrol point
			if (this.transform.position == dest.target.transform.position && dest.target.tag != "Player") 
			{
				isEnemyMoving = false;
				SelectPoint ();
			}
			
	}
		// Randomly selects a patrol point
		public void SelectPoint()
		{		
			if (!isEnemyMoving) {
				pointsIndex = Random.Range (0, patrolPoints.Capacity);
				Transform selectedPoint = patrolPoints [pointsIndex].transform;
				dest.target = selectedPoint.transform;
				isEnemyMoving = true;
			}
	
		}

	public void CheckHealth()
	{
		if (healthEnemy <= 0) {
			Destroy (this.gameObject, 0.1f);
			//gm.indexEnemies--;
			gm.enemyList.Remove (this.gameObject);
			StartCoroutine (gm.RespawnEnemies ());
		}
	}

	}
	


