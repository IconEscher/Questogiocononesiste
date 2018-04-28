using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour {

	public PlayerManager pm;
	public GameObject[] piante;
	public int arrayIndex = 0;
	public bool isGrowing = false;
	public GameObject textWin;
		
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			//Debug.Log ("Sto collidendo");
			if (!isGrowing && arrayIndex < 5)
				StartCoroutine (StartPesto ());
		} 
		if (arrayIndex == 5) 
		{
			textWin.SetActive (true);
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
