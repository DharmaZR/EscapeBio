using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputAction moveAction;

    public float moveSpeed = 6f;
    public float turnSpeed = 150f;

    private Rigidbody rb;
    private Vector2 moveInput;
    bool reverse = false;
    delegate void FreshStart();

    FreshStart freshStart;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitForStart());
        freshStart += CloseDoor;
        freshStart += UnblindPlayer;
        freshStart += ResetControls;
        freshStart();
    }

    

    void Update()
    {
        if (reverse)
        {
            moveInput = -moveAction.ReadValue<Vector2>();
        }
        else
        {
            moveInput = moveAction.ReadValue<Vector2>();
        }
    }

    void FixedUpdate()
    {
        Quaternion turnRotation = Quaternion.Euler(0f, moveInput.x * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
        Vector3 move = transform.forward * moveInput.y * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(5f);
        moveAction.Enable();
    }

    public void OpenDoor()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("door");
        foreach (GameObject door in doors)
        {
            door.GetComponent<MeshCollider>().enabled = false;
        }
    }
    public void CloseDoor()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("door");
        foreach (GameObject door in doors)
        {
            door.GetComponent<MeshCollider>().enabled = true;
        }
    }
    public void BlindPlayer()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<Camera>().fieldOfView = 45f;
    }
    public void UnblindPlayer()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<Camera>().fieldOfView = 60f;
    }
    public void ReverseControls()
    {
        reverse = true;
        StartCoroutine(ResetControls(10));
    }

    IEnumerator ResetControls(int duration)
    {
        yield return new WaitForSeconds(duration);
        reverse = false;
    }
    
    void ResetControls()
    {
        reverse = false;
    }
}
