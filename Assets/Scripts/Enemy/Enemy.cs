using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private int damage;
    
    public int Damage => damage;
}
