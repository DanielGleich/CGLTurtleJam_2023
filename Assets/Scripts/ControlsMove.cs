using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlsMove : MonoBehaviour {
    private InputActionControls inputActions;
    [SerializeField] private float moveSpeed;
    private InputAction horizontalMove;
    private InputAction verticalMove;
    private Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rb2d;

    [SerializeField] private bool horizontalMovement;
    [SerializeField] private bool verticalMovement;

    private void Awake() {
        inputActions = new InputActionControls();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        horizontalMove = inputActions.Gameplay.HorizontalMove;
        verticalMove = inputActions.Gameplay.VerticalMove;

        EnableControls();
    }

    private void OnDisable() {
        DisableControls();
    }

    private void Update() {
        if (horizontalMovement)
            moveDirection.x = horizontalMove.ReadValue<Vector2>().x;

        if (verticalMovement)
            moveDirection.y = verticalMove.ReadValue<Vector2>().y;
    }

    private void FixedUpdate() {
        Vector2 move = new(moveDirection.x, moveDirection.y);

        rb2d.velocity = moveSpeed * move;
    }

    public void EnableControls()
    {
        horizontalMove.Enable();
        verticalMove.Enable();
    }

    public void DisableControls()
    {
        horizontalMove.Disable();
        verticalMove.Disable(); 
    }
}
