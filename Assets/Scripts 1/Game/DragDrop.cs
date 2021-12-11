using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

	[SerializeField] private Canvas canvas = null;

	private RectTransform moneyBox, moneyBoxClone;
	private CanvasGroup canvasGroup;

	public AudioSource audioSourceTap; // kovanice i novcanice proizvode isti zvuk
	public AudioClip audioClipTap;

	static public float moneyNumber = 0;
	static public string moneyName;

	private GameObject Money = null;

	public bool droppedOnSlot = false;

	public Vector3 primaryPos;

	private void Start() {
        primaryPos = GetComponent<RectTransform>().localPosition;
        moneyBox.transform.localScale = new Vector3(1, 1, 1);
        Money = GameObject.FindGameObjectsWithTag("MoneyBag")[0];
        Money.transform.SetParent(canvas.transform);
    }

    private void Awake() {
        moneyBox = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //defaultPos = GetComponent<RectTransform>().localPosition;
    }

	public void OnBeginDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts = false;
		if (droppedOnSlot) {
			droppedOnSlot = false;
			moneyNumber = getMoneyNumber(moneyName);
			RemovedMoney(moneyNumber);
			audioSourceTap.clip = audioClipTap;
			audioSourceTap.Play();
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		moneyNumber = getMoneyNumber(moneyName);
		if (droppedOnSlot) {
			moneyBox.anchoredPosition = primaryPos;
		} else {
			moneyBox.anchoredPosition += eventData.delta / canvas.scaleFactor;
		}
	}
 
	public void OnEndDrag(PointerEventData eventData)
	{
		canvasGroup.blocksRaycasts = true;
		//int cloneNumber = checkClone(moneyName);
		if (droppedOnSlot) {
			//play sound
			audioSourceTap.clip = audioClipTap;
			audioSourceTap.Play();
			cloneObject();
		} else {
			this.moneyBox.localPosition = primaryPos;
		}
	}

    public void OnPointerDown(PointerEventData eventData)
	{
		//Debug.Log("OnPointerDown");
		moneyBox.anchoredPosition = primaryPos;
		int cloneCount = checkClone(moneyName);
		if (droppedOnSlot)
		{
			moneyNumber = getMoneyNumber(moneyName);
			RemovedMoney(moneyNumber);
		}
			if (cloneCount > 1)
		{
			Destroy(moneyBox.gameObject);
		}
		
	} 

    public float getMoneyNumber(string MoneyName)
	{
		moneyName = GetComponent<Image>().name;
		string moneyNumber_str = moneyName.Substring(2);
		moneyNumber = float.Parse(moneyNumber_str);
		if (moneyName[0] == 'l') moneyNumber /= 100;
		return moneyNumber;
    }

    public void RemovedMoney(float number)
	{
	    //GiveAmount.givenNumber -= (int) number;
		canvas.GetComponent<LevelManagement>().UpdateGiven(-number);
		moneyBox.gameObject.tag = "Untagged";
    	int cloneCount = checkClone(moneyName);
    	if (cloneCount != 0) {
    		Destroy(moneyBox.gameObject);
    	}
    }

    int checkClone(string objectName)
	{
		int cnt = 0;
    	foreach(GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			//Debug.Log("moneyName: " + moneyName);
			if(gameObj.name == moneyName && gameObj.tag == "Untagged") {
			     cnt++;
			}
		}
		return cnt;
    }

    void cloneObject()
	{
		moneyBoxClone = Instantiate(moneyBox);
		//moneyBoxClone.transform.localScale = new Vector3(1, 1, 1);
		moneyBoxClone.name = moneyBox.name;
		moneyBoxClone.transform.SetParent(Money.transform);
		moneyBoxClone.tag = "Untagged";
		moneyBoxClone.GetComponent<DragDrop>().droppedOnSlot = false;
		moneyBoxClone.localPosition = primaryPos;
    }
}
