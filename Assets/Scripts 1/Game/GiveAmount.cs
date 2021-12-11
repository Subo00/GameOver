using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveAmount : MonoBehaviour {
    static System.Random r = new System.Random();
    public static int amountNumber, givenNumber;
    public Text givenTxt, amountTxt; 

    void Start() {
    	amountNumber = r.Next(1, 100);
    	amountTxt.text = "" + amountNumber + " kn";
    	//Debug.Log(amount_number);
    	givenNumber = 0;
    }

    public void Update() {
    	givenTxt.text = "" + givenNumber + " kn";
        //Debug.Log("givenTxt = " + givenNumber);
        if (NextLvl.proceed) {
            givenNumber = 0;
            givenTxt.text = "" + givenNumber + " kn";
            amountNumber = r.Next(1, 100);
            amountTxt.text = "" + amountNumber + " kn";
            NextLvl.proceed = false;
        }
    }
}
