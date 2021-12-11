using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditWalletItems : MonoBehaviour
{
    [SerializeField] Wallet wallet;
    int COINS_ITEMS, BANKNOTES_ITEMS;
    bool showCoins = true;
    int currentItemIndex = 0;
    public Button walletItemPlaceholder;
    public TextMeshProUGUI description;
    public TMP_InputField quantityInput;

    void Start()
    {
        string jsonString = System.IO.File.ReadAllText(StartMenu.walletSavePath);
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        COINS_ITEMS = wallet.coins.Count;
        BANKNOTES_ITEMS = wallet.banknotes.Count;
        Sprite sprite = Resources.Load<Sprite>("Money/" + wallet.coins[currentItemIndex].imageAPath);
        SetCurrentItem(wallet.coins[currentItemIndex], sprite);
    }

    void SetCurrentItem(WalletItem walletItem, Sprite sprite)
    {
        walletItemPlaceholder.image.sprite = sprite;
        description.text = walletItem.name + "\nUkupna vrijednost: " + (walletItem.quantity * walletItem.value).ToString() + " kn";
        quantityInput.text = walletItem.quantity.ToString();
    }

    public void UpdateCurrentItemIndex(int offset)
    {
        currentItemIndex += offset;
        if (showCoins)
        {
            currentItemIndex = (currentItemIndex >= 0) ? currentItemIndex % COINS_ITEMS : COINS_ITEMS-1;
            Sprite sprite = Resources.Load<Sprite>("Money/" + wallet.coins[currentItemIndex].imageAPath);
            SetCurrentItem(wallet.coins[currentItemIndex], sprite);
        }
        else
        {
            currentItemIndex = (currentItemIndex >= 0) ? currentItemIndex % BANKNOTES_ITEMS : BANKNOTES_ITEMS-1;
            Sprite sprite = Resources.Load<Sprite>("Money/" + wallet.banknotes[currentItemIndex].imageAPath);
            SetCurrentItem(wallet.banknotes[currentItemIndex], sprite);
        }
    }

    public void SetShowCoins(bool sc)
    {
        if (showCoins != sc)
        {
            currentItemIndex = 0;
            WalletItem walletItem = sc ? wallet.coins[currentItemIndex] : wallet.banknotes[currentItemIndex];
            Sprite sprite = Resources.Load<Sprite>("Money/" + walletItem.imageAPath);
            SetCurrentItem(walletItem, sprite);
        }
        showCoins = sc;
    }

    public void UpdateQuantity(int offset)
    {        
        if (showCoins)
        {
            wallet.coins[currentItemIndex].quantity = System.Math.Max(0, wallet.coins[currentItemIndex].quantity + offset);
            Sprite a = Resources.Load<Sprite>("Money/" + wallet.coins[currentItemIndex].imageAPath);
            Sprite b = Resources.Load<Sprite>("Money/" + wallet.coins[currentItemIndex].imageBPath);
            SetCurrentItem(wallet.coins[currentItemIndex], (walletItemPlaceholder.image.sprite == a) ? a : b);
        }
        else
        {
            wallet.banknotes[currentItemIndex].quantity = System.Math.Max(0, wallet.banknotes[currentItemIndex].quantity + offset);
            Sprite a = Resources.Load<Sprite>("Money/" + wallet.banknotes[currentItemIndex].imageAPath);
            Sprite b = Resources.Load<Sprite>("Money/" + wallet.banknotes[currentItemIndex].imageBPath);
            SetCurrentItem(wallet.banknotes[currentItemIndex], (walletItemPlaceholder.image.sprite == a) ? a : b);
        }
    }

    public void UpdateQuantityFromInput()
    {
        int value = System.Math.Abs(System.Int32.Parse(quantityInput.text));
        if (showCoins)
        {
            UpdateQuantity(value - wallet.coins[currentItemIndex].quantity);
        }
        else
        {
            UpdateQuantity(value - wallet.banknotes[currentItemIndex].quantity);
        }
    }

    public void SaveToJson()
    {
        System.IO.File.WriteAllText(StartMenu.walletSavePath, JsonUtility.ToJson(wallet));
    }

    public void FlipMoney()
    {
        WalletItem currentWalletItem = (showCoins) ? wallet.coins[currentItemIndex] : wallet.banknotes[currentItemIndex];
        Sprite a = Resources.Load<Sprite>("Money/" + currentWalletItem.imageAPath);
        Sprite b = Resources.Load<Sprite>("Money/" + currentWalletItem.imageBPath);
        walletItemPlaceholder.image.sprite = (walletItemPlaceholder.image.sprite == a) ? b : a;
    }
}
