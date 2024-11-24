using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Rigidbody2D rigidbody2D;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float moveDistance;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var movementVector = new Vector2(0f, playerInput.VerticalInput) * (speed * Time.fixedDeltaTime);
        
        var clampPosition = rigidbody2D.position + movementVector;
        clampPosition.y = Mathf.Clamp(clampPosition.y, -moveDistance, moveDistance);  // ограничивает значение y новой позиции (clampPosition.y) в пределах диапазона

        rigidbody2D.MovePosition(clampPosition);
    }
}
