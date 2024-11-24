using System.Collections;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator playerAnimator;

    // хэш имени анимации
    private readonly int idleAnimationHash = Animator.StringToHash("PlayerIdle");
    private readonly int runAnimationHash = Animator.StringToHash("PlayerRun");
    private readonly int fadeAnimationHash = Animator.StringToHash("PlayerFade");

    private bool isFading;

    private void OnEnable()
    {
        player.OnDamageTakeEvent += OnDamageTake;
    }

    private void Update()
    {
        // если анимация "попадание врага" вкл.
        if (isFading)
        {
            return;
        }

        // если было движение игрока
        if (playerInput.VerticalInput != 0f)
        {
            playerAnimator.CrossFade(runAnimationHash, 0f);
        }
        else
        {
            playerAnimator.CrossFade(idleAnimationHash, 0f);
        }
    }

    private void OnDisable()
    {
        player.OnDamageTakeEvent -= OnDamageTake;
    }

    private void OnDamageTake()
    {
        isFading = true;
        playerAnimator.CrossFade(fadeAnimationHash, 0f);
        StartCoroutine(ResetFadeState());
    }

    private IEnumerator ResetFadeState()
    {
        // пауза на длину текущей анимации, чтобы избежать прерывания до её завершения
        yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length);
        isFading = false;
    }
}
