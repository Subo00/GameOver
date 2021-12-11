using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListController : MonoBehaviour
{
    [SerializeField] Wallet wallet;
    public GameObject ContentPanel, ListItemPrefab;

    void Start()
    {
        string jsonString = System.IO.File.ReadAllText(StartMenu.walletSavePath);
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        for (int i = wallet.banknotes.Count-1; i >= 0; i--)
        {
            if (wallet.banknotes[i].quantity == 0) continue;
            GameObject walletItem = Instantiate(ListItemPrefab) as GameObject;
            ListItemController controller = walletItem.GetComponent<ListItemController>();
            controller.image.sprite = Resources.Load<Sprite>("Money/" + wallet.banknotes[i].imageAPath);
            controller.quantity.text = "x" + wallet.banknotes[i].quantity.ToString();
            walletItem.transform.SetParent(ContentPanel.transform);
            walletItem.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        }
        for (int i = 0; i < wallet.coins.Count; i++)
        {
            if (wallet.coins[i].quantity == 0) continue;
            GameObject walletItem = Instantiate(ListItemPrefab) as GameObject;
            ListItemController controller = walletItem.GetComponent<ListItemController>();
            controller.image.sprite = Resources.Load<Sprite>("Money/" + wallet.coins[i].imageAPath);
            controller.quantity.text = "x" + wallet.coins[i].quantity.ToString();
            walletItem.transform.SetParent(ContentPanel.transform);
            walletItem.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }
    }
}
