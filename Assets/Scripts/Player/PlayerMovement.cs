using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Inputs input;
    private Rigidbody2D rb;
    private Vector2 direction; 
    private PlayerGrowth playerGrowth; 

    private void Awake()
    {
        input = new Inputs();
        
        rb = GetComponent<Rigidbody2D>();
        playerGrowth = GetComponent<PlayerGrowth>(); 
        direction = Vector2.zero; 
    }

    private void Update()
    {
        rb.linearVelocity = direction.normalized * moveSpeed * playerGrowth.SizeScaleRatio(); 
    }

    private void OnEnable()
    {
        input.Enable(); 

        input.Player.Move.performed += Move;
        input.Player.Move.canceled += Move;
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= Move;
        input.Player.Move.canceled -= Move;

        input.Disable(); 
    }

    private void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>(); 
    }
}
