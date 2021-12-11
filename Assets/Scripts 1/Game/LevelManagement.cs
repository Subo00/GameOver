using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManagement : MonoBehaviour
{
	//[SerializeField] LevelList levelList;
	//List<Level> loadedLevels;
	//Random rnd = new Random();
	string difficulty;
	int currentLevel;
	int levelNumber = 0; // broj levela kroz koje je korisnik prosao
	int rightAnswer = 0;
	float amountNumber, givenNumber = 0;
	public TextMeshProUGUI levelTxt, amountTxt, givenTxt, resultTxt;
	public AudioSource audioSourceT;
	public AudioClip audioClipT;
	public AudioSource audioSourceN;
	public AudioClip audioClipN;

	public GameObject gameComplete, lipe, piggyLvl, wrongAnswerScreen;

	void Start()
    {
        //string jsonString = System.IO.File.ReadAllText(StartMenu.levelsSavePath);
        //levelList = JsonUtility.FromJson<LevelList>(jsonString);
        difficulty = PlayerPrefs.GetString("gameDifficulty");
		//loadedLevels = (difficulty == "EasyGame") ? levelList.easy : levelList.hard;
		givenTxt.enabled = (PlayerPrefs.GetString("helpTextVisibilty") == "true");
		//maxLevel = loadedLevels.Count - 1;
		//currentLevel = System.Math.Max(0, loadedLevels.FindIndex(NotCompleted));
		currentLevel = 0;
		SetLevel();
	}

	bool NotCompleted(Level l)
    {
		return !l.completed;
    }

	void SetLevel()
    {
		levelTxt.text = (currentLevel + 1).ToString();
		if (difficulty != "EasyGame") {
    		lipe.gameObject.SetActive(true);
			amountNumber = Random.Range(1.2f, 650.2f);
			amountTxt.text = "Teta prodavačica traži " + amountNumber.ToString("F2") + "kn";
		}
		//amountNumber = loadedLevels[currentLevel].goalAmount;
		else
		{
			amountNumber = Random.Range(1, 650);
			amountTxt.text = "Teta prodavačica traži " + amountNumber.ToString() + "kn";
		}
		UpdateGiven(-givenNumber);
	}

	public void UpdateGiven(float offset)
    {
		givenNumber += offset;
		if (difficulty != "EasyGame") {
			givenTxt.text = "Za sada imaš " + givenNumber.ToString("F2") + "kn";
		} else {
			givenTxt.text = "Za sada imaš " + givenNumber.ToString() + "kn";
		}
	}

	public void ProceedLevel()
	{
		if (amountNumber - givenNumber < 0.0001)
		{
			RightAnswer();
			currentLevel++;
			SetLevel();
			DeleteObjects();
		}
		else
		{
			WrongAnswer();
		}
	}

	void DeleteObjects()
	{
		GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("cashReg");
		foreach (GameObject gameObject in foundObjects)
		{
			Destroy(gameObject);
		}
	}

	void RightAnswer()
	{	
		audioSourceT.clip = audioClipT;
		audioSourceT.Play();
		rightAnswer++;
		StartCoroutine(piggyExpand());
		
		//Debug.Log("right answer");
		//Debug.Log("broj tocnih" + rightAnswer);
	}

	IEnumerator piggyExpand()
	{
		piggyLvl.gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 1);

		//Wait for 1 seconds
		yield return new WaitForSeconds(0.7f);

		//Rotate 40 deg
		piggyLvl.gameObject.transform.localScale = new Vector3(1, 1, 1);
	}

	
	void WrongAnswer()
	{
		audioSourceN.clip = audioClipN;
		audioSourceN.Play();
		StartCoroutine(closeWrongAnswer());
	}

	IEnumerator closeWrongAnswer()
	{
		wrongAnswerScreen.gameObject.SetActive(true);
		yield return new WaitForSeconds(5);	
		wrongAnswerScreen.gameObject.SetActive(false);
	}

	void closePopUp()
    {
		wrongAnswerScreen.gameObject.SetActive(false);
	}

	void EndGame()
	{

		if (PlayerPrefs.GetInt("highscoreEasy") < rightAnswer)
		{
			PlayerPrefs.SetInt("highscoreEasy", rightAnswer);
		}
		gameComplete.gameObject.SetActive(true);
		GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
		float scorePers = (float)rightAnswer / ((float)levelNumber + 1);
		int starNum = 0;
		if (scorePers > 0.800001) starNum = 5;
		else if (scorePers <= 0.800001 && scorePers > 0.600001) starNum = 4;
		else if (scorePers <= 0.600001 && scorePers > 0.400001) starNum = 3;
		else if (scorePers <= 0.400001 && scorePers > 0.200001) starNum = 2;
		else if (scorePers > 0) starNum = 1;
		else starNum = 0;
		for (int i  = 0; i < starNum; i++) {
			if (int.Parse(stars[i].name) == i+1) {
				 stars[i].transform.GetChild(0).gameObject.SetActive(true);
			}
		}
		resultTxt.text = "Točnih odgovora: " + rightAnswer + "/" + (levelNumber+1);
	}
}
