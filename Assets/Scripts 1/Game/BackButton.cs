using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {

    public void backToGameMenu() {
    	SceneManager.LoadScene("StartMenu");
    	NextLvl.levelNumber = 1;
		NextLvl.rightAnswer = 0;
		NextLvl.proceed = false;
    }
}
