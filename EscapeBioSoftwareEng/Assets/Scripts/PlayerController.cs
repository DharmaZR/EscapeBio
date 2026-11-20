using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public InputAction moveAction;

    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float turnSpeed = 150f;

    private Rigidbody rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction.Enable();
    }

    

    void Update()
    {
            moveInput = moveAction.ReadValue<Vector2>();
        
    }

    void FixedUpdate()
    {
        float turnAmount = moveInput.x * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
        Vector3 forwardMovement = transform.forward * (moveInput.y * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + forwardMovement);
    }
}
