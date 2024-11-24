using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    
    private int score;
    private int currentHealth;

    public event UnityAction<int> OnScoreChangeEvent;
    public event UnityAction<int> OnHealthChangeEvent;
    public event UnityAction OnDamageTakeEvent;

    private void Start()
    {
        currentHealth = health;
        OnHealthChangeEvent?.Invoke(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            score += coin.CoinValue;
            OnScoreChangeEvent?.Invoke(score);
        }
        else if (collision.TryGetComponent(out Enemy enemy))
        {
            if (enemy.Damage < 0)
            {
                return;
            }

            if (currentHealth - enemy.Damage >= 0)
            {
                currentHealth -= enemy.Damage;
                OnHealthChangeEvent?.Invoke(currentHealth);
            }

            OnDamageTakeEvent?.Invoke();
        }
        else if (collision.TryGetComponent(out Heart heart))
        {
            if (currentHealth + heart.HealValue <= health)
            {
                currentHealth += heart.HealValue;
                OnHealthChangeEvent?.Invoke(currentHealth);
            }
        }
    }
}
