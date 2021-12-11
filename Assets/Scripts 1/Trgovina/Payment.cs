using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

public class Payment : MonoBehaviour
{
    [SerializeField] Wallet wallet;
    public GameObject[] moneyList;
    public TextMeshProUGUI[] quantityList;
    public TextMeshProUGUI noMoney;
    public TextMeshProUGUI ostatakTMPro;

    private float[,] novcanik = {
                                    {0.01f, 0},
                                    {0.02f, 0},
                                    {0.05f, 0},
                                    {0.1f, 0},
                                    {0.2f, 0},
                                    {0.5f, 0},
                                    {1f, 0},
                                    {2f, 0},
                                    {5f, 0},
                                    {10f, 0},
                                    {20f, 0},
                                    {50f, 0},
                                    {100f, 0},
                                    {200f, 0},
                                    {500f, 0},
                                    {1000f, 0},
                                };

    void setUpNovcanik()
    {
        string jsonString = System.IO.File.ReadAllText(StartMenu.walletSavePath);
        wallet = JsonUtility.FromJson<Wallet>(jsonString);

        int coins = wallet.coins.Count;
        for (int i = 0; i < coins; i++)
        {
            if (wallet.coins[coins - 1 - i].quantity == 0) continue;
            novcanik[i, 1] = wallet.coins[coins - 1 - i].quantity;
        }
        for (int i = 0; i < wallet.banknotes.Count; i++)
        {
            if (wallet.banknotes[i].quantity == 0) continue;
            novcanik[i + 9, 1] = wallet.banknotes[i].quantity;
        }
    }

    int sljedecaNiza(float ostatak)
    {
        for (int i = 15; i >= 0; i--)
        {
            if (novcanik[i, 0] <= ostatak && novcanik[i, 1] > 0) return i;
        }

        return -1;
    }

    float oduzmiOdOstatka(int novcanica, float ostatak)
    {
        ostatak -= novcanik[novcanica, 0];
        novcanik[novcanica, 1] -= 1;

        return ostatak;
    }

    float sljedecaVisa(float ostatak)
    {
        for (int i = 0; i < 16; i++)
        {
            if (novcanik[i, 1] > 0)
            {
                ostatak -= novcanik[i, 0];
                novcanik[i, 1] -= 1;
            }
        }

        return ostatak;
    }


    void Start()
    {
        int n = 200;
        int i = 0;
        float iznos = Trgovina.amount;
        float ost = iznos;

        setUpNovcanik();

        if (Trgovina.krupno)   //krupno placanje - bez kovanica
        {
            for (int j = 0; j < 9; j++)
                novcanik[j, 1] = 0;
        }

        float[,] novcanik_pocetak = novcanik.Clone() as float[,];
        float[,] payment = novcanik.Clone() as float[,];

        for (int j = 0; j < 16; j++)
            iznos -= (novcanik[j, 0] * novcanik[j, 1]);

        if ((float)Math.Round(iznos, 2) > 0)
        {
            ost = 0;
            noMoney.enabled = true;
            noMoney.gameObject.SetActive(true);
        }


        while (ost > 0)
        {
            if (i != -1) i = sljedecaNiza(ost); //ima nizih novcanica
            if (i == -1) ost = sljedecaVisa(ost); //nema nizih novcanica
            else ost = oduzmiOdOstatka(i, ost);

            ost = (float)Math.Round(ost, 2);

            Debug.Log(ost);
            n -= 1;
            if (n == 0) break;
        }

        for (int j = 0; j < 16; j++)
            payment[j, 1] = novcanik_pocetak[j, 1] - novcanik[j, 1];

        if (i == -1)
        {
            for (int j = 15; j >= 0; j--)
            {
                if (payment[j, 1] > 0 && ost + payment[j, 0] <= 0)
                {
                    int broj_novcanica = (int)payment[j, 1];
                    for (int k = 1; k <= broj_novcanica; k++)
                    {
                        if (ost + payment[j, 0] > 0) break;
                        ost += payment[j, 0];
                        payment[j, 1] -= 1;
                        novcanik[j, 1] += 1;
                    }
                }
            }
        }

        MoneyChange(ost);

        for (int k = 0; k < 16; k++)
        {
            if (payment[k, 1] == 0) moneyList[k].SetActive(false);
            else quantityList[k].text = "x" + payment[k, 1].ToString();
        }
    }

    public void MoneyChange(float o)
    {
        o = (float)Math.Round(o, 2);
        ostatakTMPro.text = "Ostatak: " + Math.Abs(o).ToString() + " Kn";
    }

    public void Pay()
    {
        if (!Trgovina.krupno) //krupno placanje - bez kovanica
        {
            int coins = wallet.coins.Count;
            for (int i = 0; i < coins; i++)
                wallet.coins[coins - 1 - i].quantity = (int)novcanik[i, 1];
        }

        for (int i = 0; i < wallet.banknotes.Count; i++)
            wallet.banknotes[i].quantity = (int)novcanik[i + 9, 1];

        System.IO.File.WriteAllText(StartMenu.walletSavePath, JsonUtility.ToJson(wallet));
        SceneManager.LoadScene("Trgovina");
    }

}