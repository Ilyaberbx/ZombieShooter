using System;
using UnityEngine;

public class Wallet 
{
    #region Coin

    public Action<int> OnCoinsCountChange;

    private readonly string COINS = "Coins";
    private int _coins;
    public int Coins => _coins;
    public void AddCoins(int amount)
    {
        if (amount <= 0) return;
        _coins += amount;
        Debug.Log(_coins);
        Save();
        OnCoinsCountChange?.Invoke(_coins);
    }

    public void SubtractCoins(int amount)
    {
        if (_coins < amount) return;
        _coins -= amount;
        Save();
        OnCoinsCountChange?.Invoke(_coins);
    }
    #endregion

    private void Save()
    {
        PlayerPrefs.SetInt(COINS, _coins);
    }
    public Wallet()
    {
        _coins = PlayerPrefs.GetInt(COINS, 0);
        Debug.Log(_coins);
    }
}
