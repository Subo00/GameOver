using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.IO;

public class PopUpNovcanice : MonoBehaviour
{
    [SerializeField] Wallet wallet;
    public TMP_InputField[] banknotesInput;
    public Button btnIncrease;
    public Button btnDecrease;
    private string filePath;

    void ShowQuantity()
    {
        string jsonString = System.IO.File.ReadAllText(StartMenu.walletSavePath);
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        for (int i = 0; i < 7; i++)
            banknotesInput[i].text = wallet.banknotes[6-i].quantity.ToString();
    }

    void RefreshQuantity()
    {
        System.IO.File.WriteAllText(StartMenu.walletSavePath, JsonUtility.ToJson(wallet));
        ShowQuantity();
    }

    public void ChangeQuantity(int index)
    {
        if (index >= 0) wallet.banknotes[6-index].quantity += 1;

        else if (wallet.banknotes[6 - Math.Abs(index + 1)].quantity != 0)
            wallet.banknotes[6 - Math.Abs(index + 1)].quantity -= 1;

        RefreshQuantity();
    }

    public void ValueChangeCheck(int index)
    {
        if (banknotesInput[index].text.Length == 0) banknotesInput[index].text = "0";
        wallet.banknotes[6-index].quantity = Math.Abs(System.Int32.Parse(banknotesInput[index].text));
        RefreshQuantity();
    }

    void Start()
    {
        ShowQuantity();
    }

}
