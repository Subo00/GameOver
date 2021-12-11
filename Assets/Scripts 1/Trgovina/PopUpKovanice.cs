using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class PopUpKovanice : MonoBehaviour
{
    [SerializeField] Wallet wallet;
    public TMP_InputField[] coinsInput;
    public Button btnIncrease;
    public Button btnDecrease;

    void ShowQuantity()
    {
        string jsonString = System.IO.File.ReadAllText(StartMenu.walletSavePath);
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        for (int i = 0; i < 9; i++)
            coinsInput[i].text = wallet.coins[i].quantity.ToString();
    }

    void RefreshQuantity()
    {
        System.IO.File.WriteAllText(StartMenu.walletSavePath, JsonUtility.ToJson(wallet));
        ShowQuantity();
    }

    public void ChangeQuantity(int index)
    {
        if (index >= 0) wallet.coins[index].quantity += 1;

        else if(wallet.coins[Math.Abs(index + 1)].quantity != 0)
            wallet.coins[Math.Abs(index + 1)].quantity -= 1;

        RefreshQuantity();
    }

    public void ValueChangeCheck(int index)
    {
        if (coinsInput[index].text.Length == 0) coinsInput[index].text = "0";
        wallet.coins[index].quantity = Math.Abs(System.Int32.Parse(coinsInput[index].text));
        RefreshQuantity();
    }

    void Start()
    {
        ShowQuantity();     
    }

}
