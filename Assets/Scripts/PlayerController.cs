using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float forewardMoveSpeed = 0;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject WinTextObject;
    public GameObject Pickup;

    private Rigidbody rb;
    private Transform tf;
    private int count;
    private float movementX;
    private float movementY;
    
    

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        SetCountText();
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        WinTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x * speed;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count == 12)
        {
            WinTextObject.SetActive(true);
        }
    }


    void FixedUpdate()
    { 
        Vector3 movement = new Vector3(movementX, 0.0f, forewardMoveSpeed);

        rb.AddForce(movement);

        forewardMoveSpeed = (float)(forewardMoveSpeed * 0.99);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }

}
