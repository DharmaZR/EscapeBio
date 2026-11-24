using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputAction moveAction;

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
        Quaternion turnRotation = Quaternion.Euler(0f, moveInput.x * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
        Vector3 move = transform.forward * moveInput.y * moveSpeed * Time.fixedDeltaTime;
        transform.Translate(move, Space.World);
    }
}
