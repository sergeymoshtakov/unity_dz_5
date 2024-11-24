using UnityEngine;

public class Coin : Collectible
{
    [SerializeField] private int coinValue;
    
    public int CoinValue => coinValue;
}