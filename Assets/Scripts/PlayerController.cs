using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject WinTextObject;
    public GameObject Pickup;
    public GameObject Obstacle;
    public Transform SpawnLocation;
    public int spawnDifficulty = 1;

    private float forewardMoveSpeed = 15;
    private Rigidbody rb;
    private Transform tf;
    private int count;
    private float movementX;
    private float movementY;
    private int spawnTime = 100;

    


    // Start is called before the first frame update
    void Start()
    {

        count = 0;
        SetCountText();
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        WinTextObject.SetActive(false);
        rb.AddForce(0, 0, 2000);
}

    //Creates a vector based on keypresses to move the player
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x * speed;
        movementY = movementVector.y;
    }

    //Sets the On Screen Score 
    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();
        if (count == 100)
        {
            WinTextObject.SetActive(true);
        }
    }




    
    void FixedUpdate()
    {
        //Adds Force to the Player Based on the Movement Vector
        Vector3 movement = new Vector3(movementX, 0.0f, 0.0f);

        rb.AddForce(movement);

        rb.AddForce(0, 0, forewardMoveSpeed);




        //spawns obstacles and pickups at a random position on the track based on the timer

        if (spawnTime == 49)
        {
            Instantiate(Obstacle, SpawnLocation.position, SpawnLocation.rotation);
            Debug.Log("Spawned Obstacle");
        }

        if (spawnTime == 5)
        {
            Instantiate(Pickup, SpawnLocation.position, SpawnLocation.rotation);
            Debug.Log("Spawned Pickup");
        }

        //Timer used to spawn Obstacles in front of the player (Not sure if there is a better way to implement this)
        if (spawnTime < 1)
        {
            spawnTime = 100 / spawnDifficulty;
        }
        else
        {
            spawnTime--;
        }
   
        //Checks if the Player has fallen off the map and restarts the level. 
        if (rb.position.y < -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

       
    }

    //Dectects collsions with pickup items
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }

    //Detects collision with obstacles and restarts scene.
    private void OnCollisionEnter(Collision collisionInfo)
    {

        if (collisionInfo.collider.tag == "Obstacle")
        {
            Debug.Log("Collision");

            //I don't know how to use the scene manager so I found this on the unity forums. (Also line 38)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
   