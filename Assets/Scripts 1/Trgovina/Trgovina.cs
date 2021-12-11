using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class Trgovina : MonoBehaviour
{
    [SerializeField] Wallet wallet;
    int COINS_ITEMS, BANKNOTES_ITEMS;
    public TextMeshProUGUI total;

    static public float amount;
    static public Boolean krupno;
    public GameObject inputField;
    public TextMeshProUGUI input_price;
    private int lipe;

    void Start()
    {
        lipe = -1;
        krupno = false;
        string jsonString = System.IO.File.ReadAllText(StartMenu.walletSavePath);
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        COINS_ITEMS = wallet.coins.Count;
        BANKNOTES_ITEMS = wallet.banknotes.Count;
        total.text = String.Format("U novčaniku:\n{0:0.00} kn", GetTotalValue());
    }

    void Update()
    {
        string input_txt = inputField.GetComponent<TMP_InputField>().text;
        if (input_txt.Length > 0) amount = float.Parse(input_txt, System.Globalization.CultureInfo.GetCultureInfo("hr-HR").NumberFormat);
        else amount = 0;
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

    public void LargePayment()
    {
        krupno = true;
        SceneManager.LoadScene("Placanje");
    }

    public void WriteNum(int n)
    {
        TMP_InputField input = inputField.GetComponent<TMP_InputField>();
        input.interactable = true;

        if (n == -1)
        {
            if (input.text.Length > 0) input.text = input.text.Remove(input.text.Length - 1, 1);
            if (lipe >= 0) lipe += 1;
            if (lipe == 3) lipe = -1;
        }

        else if (n == -2 && lipe < 0)
        {
            if (input.text.Length == 0) input.text = "0";
            input.text += ",";
            lipe = 2;
        }

        else if (n >= 0 && lipe != 0)
        {
            input.text += n.ToString();
            if (lipe > 0) lipe -= 1;
        }
        if (input.text == "0") input.text = input.text.Remove(input.text.Length - 1, 1);
    }
}
