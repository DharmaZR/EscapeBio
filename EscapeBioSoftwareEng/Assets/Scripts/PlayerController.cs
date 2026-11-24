using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public InputAction moveAction;
    public TextMeshProUGUI keyText;
    public Button replay;

    public float moveSpeed = 6f;
    public float turnSpeed = 150f;

    private Rigidbody rb;
    private Vector2 moveInput;
    bool reverse = false;
    delegate void FreshStart();

    FreshStart freshStart;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitForStart());
        freshStart += CloseDoor;
        freshStart += UnblindPlayer;
        freshStart += ResetControls;
        freshStart();
        keyText.gameObject.SetActive(false);
        replay.gameObject.SetActive(false);
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
            door.SetActive(false);
        }
        keyText.text = "Doors opened";
        keyText.gameObject.SetActive(true);
        StartCoroutine(HideKeyText());
    }
    public void CloseDoor()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("door");
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }
    public void BlindPlayer()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<Camera>().fieldOfView = 45f;
        keyText.text = "Your vision is changed";
        keyText.gameObject.SetActive(true);
        StartCoroutine(HideKeyText());
    }
    public void UnblindPlayer()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<Camera>().fieldOfView = 60f;
    }
    public void ReverseControls()
    {
        reverse = true;
        keyText.text = "Your controls are reversed";
        keyText.gameObject.SetActive(true);
        StartCoroutine(HideKeyText());
        StartCoroutine(ResetControls(15));
        
    }

    IEnumerator ResetControls(int duration)
    {
        yield return new WaitForSeconds(duration);
        reverse = false;
    }
    IEnumerator HideKeyText()
    {
        yield return new WaitForSeconds(2f);
        keyText.gameObject.SetActive(false);
    }

    void ResetControls()
    {
        reverse = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("exit"))
        {
            moveAction.Disable();
            keyText.text = "You have escaped!";
            keyText.gameObject.SetActive(true);
            replay.gameObject.SetActive(true);
        }
    }

    public void OnClick()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
