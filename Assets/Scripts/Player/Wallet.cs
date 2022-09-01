using UnityEngine;

public class Wallet 
{
    #region Coin

    private readonly string COINS = "Coins";
    private int _coins;
    public int Coins => _coins;

    public void AddCoins(int amount)
    {
        if (amount <= 0) return;

        _coins += amount;

        Save();
    }
    #endregion

    private void Save()
    {
        PlayerPrefs.SetInt(COINS, _coins);
    }
}
