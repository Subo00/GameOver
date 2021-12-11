using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CashRegister : MonoBehaviour, IDropHandler
{
	public Canvas canvas;

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag != null && !eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot)
		{
			// GiveAmount.givenNumber += DragDrop.moneyNumber;
			canvas.GetComponent<LevelManagement>().UpdateGiven(DragDrop.moneyNumber);
			eventData.pointerDrag.GetComponent<DragDrop>().droppedOnSlot = true;
			eventData.pointerDrag.GetComponent<RectTransform>().gameObject.tag = "cashReg";
		}
	} 
}
