using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalValue : MonoBehaviour
{
    [SerializeField] Wallet wallet;
    int COINS_ITEMS, BANKNOTES_ITEMS;
    public TextMeshProUGUI total;

    void Start()
    {
        string jsonString = System.IO.File.ReadAllText(StartMenu.walletSavePath);
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        COINS_ITEMS = wallet.coins.Count;
        BANKNOTES_ITEMS = wallet.banknotes.Count;
        total.text = "UKUPNO: " + GetTotalValue().ToString() + " kn";
    }

    double GetTotalValue()
    {
        double totalValue = 0;
        for (int i = 0; i < COINS_ITEMS; i++)
        {
            totalValue += wallet.coins[i].quantity * wallet.coins[i].value;
        }
        for (int i = 0; i < BANKNOTES_ITEMS; i++)
        {
            totalValue += wallet.banknotes[i].quantity * wallet.banknotes[i].value;
        }
        return totalValue;
    }
}
