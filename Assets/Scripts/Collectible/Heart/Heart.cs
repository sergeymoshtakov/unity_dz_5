using UnityEngine;

public class Heart : Collectible
{
    [SerializeField] private int healValue;

    public int HealValue => healValue;
}
