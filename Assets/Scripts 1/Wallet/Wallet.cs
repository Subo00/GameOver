using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wallet
{
    public List<WalletItem> coins = new List<WalletItem>();
    public List<WalletItem> banknotes = new List<WalletItem>();
}

[System.Serializable]
public class WalletItem
{
    public string name;
    public double value;
    public int quantity;
    public string imageAPath;
    public string imageBPath;
}
