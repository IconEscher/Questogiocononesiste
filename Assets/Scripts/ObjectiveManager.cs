using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour {

	//PlayerManager pm;
	public GameObject[] piante;
	public int arrayIndex = 0;
	public bool isGrowing = false;
    //public GameObject textWin;
    public GameObject pesto;
    public Canvas endGameCanvas;
    Image victoryImage;
    public Image lossImage;

    void Start()
    {
        
        pesto = GameObject.Find("Pesto");
        victoryImage = GameObject.Find("VictoryImage").GetComponent<Image>();
        lossImage = GameObject.Find("LossImage").GetComponent<Image>();
        endGameCanvas = GameObject.Find("EndGameCanvas").GetComponent<Canvas>();
        pesto.SetActive(false);
        victoryImage.gameObject.SetActive(false);
        lossImage.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(false);
    }
		
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			//Debug.Log ("Sto collidendo");
			if (!isGrowing && arrayIndex < 5)
				StartCoroutine (StartPesto ());
		} 
		if (arrayIndex == 5) 
		{
            StartCoroutine(HasWon());
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

    public IEnumerator HasWon()
    {
        pesto.SetActive(true);
        PlayerManager pm = GameObject.Find("Player").GetComponent<PlayerManager>();
        pm.invulnerable = true;
        yield return new WaitForSeconds(2);
        endGameCanvas.gameObject.SetActive(true);
        victoryImage.gameObject.SetActive(true);
        
    }
}
