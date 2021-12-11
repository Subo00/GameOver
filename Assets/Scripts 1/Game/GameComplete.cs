using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameComplete : MonoBehaviour
{
	public GameObject gameComplete;
    
    private void showEndScrene() {
    	if (NextLvl.levelNumber == NextLvl.maxLevel) {
    		gameComplete.gameObject.SetActive(true);
    	}
    }
}
