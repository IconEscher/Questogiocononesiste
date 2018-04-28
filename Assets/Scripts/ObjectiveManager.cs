using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour {

	public PlayerManager pm;
	public GameObject[] piante;
	public int arrayIndex = 0;
	public bool isGrowing = false;
		
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			Debug.Log ("Sto collidendo");
			if (!isGrowing && arrayIndex < 6)
				StartCoroutine (StartPesto ());
		} else if (arrayIndex == 5) 
		{
			
		}

	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			StartCoroutine (StopPesto ());
		}

	}

	public IEnumerator StartPesto()
	{
		isGrowing = true;
		piante [arrayIndex].SetActive (false);
		arrayIndex++;
		piante [arrayIndex].SetActive (true);
		yield return new WaitForSeconds (1);
		isGrowing = false;
	}

	public IEnumerator StopPesto()
	{
		piante [arrayIndex].SetActive (false);
		arrayIndex = 0;
		piante [arrayIndex].SetActive (true);
		yield return null;
	}
}
