using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour {

	public Text levelTxt;
    public static int maxLevel; 
    public static int rightAnswer = 0;
    public static int levelNumber = 1;
    public static bool proceed = false;


    void Awake() {
    	levelTxt.text = "Level: " + levelNumber + "/" + maxLevel;
    }

	public void proceedLvl() {
		//Debug.Log(GiveAmount.givenNumber);
		//Debug.Log(GiveAmount.amountNumber);

		if (GiveAmount.givenNumber == GiveAmount.amountNumber) RightAnswer();
		else WrongAnswer();

		if (levelNumber == maxLevel) EndGame();

		levelNumber += 1;
		proceed = true;
		levelTxt.text = "Level: " + levelNumber + "/" + maxLevel;
		DeleteObjects();
	}

	void DeleteObjects() {
		GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("cashReg");
		foreach (GameObject gameObject in foundObjects) {
			Destroy(gameObject);
		}
	}

	void RightAnswer() {
		// ZVUK BRAVO
		rightAnswer++;
		Debug.Log("broj tocnih" + rightAnswer);
	}

	void WrongAnswer() {
		/// ZVUK KRIVO! I PORUKA NA PAR SEKUNDI - KRIVO, ALI SAMO HRABRO NAPRIJED?
	}

	void EndGame() {
		if (PlayerPrefs.GetInt("highscoreEasy") < rightAnswer) {
			PlayerPrefs.SetInt("highscoreEasy", rightAnswer);
		}
		//Debug.Log("broj tocnih NA KRAJU " + rightAnswer);
		levelNumber = 1;
		rightAnswer = 0;
		//SceneManager.LoadScene("StartMenu");
	}
}
