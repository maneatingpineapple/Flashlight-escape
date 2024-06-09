using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    public GameObject flashlight;
    public BatteryBar batteryBar;
    public float maxBattery = 100;
    public float currentBattery = 100;
    public float batDecreaseRate = 1;
    bool flashlightOn = false;

    public Rigidbody2D rb;
    Vector2 movement;
    public float moveSpeed = 5f;

    Vector3 mousePosition;
    Vector2 direction;
    float angle;

    public AudioSource audioSource;

    public AudioClip charge;
    public AudioClip getKey;
    public AudioClip openDoor;

    bool hasKey = false;

    public GameObject ghostPrefab;

    public static int room = 0;

    public RoomGenerator roomGen;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        batteryBar.SetMaxBattery(maxBattery);
        batteryBar.SetBattery(currentBattery);
        flashlight.SetActive(flashlightOn);
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = new Vector2(movement.x, movement.y).normalized;
        //turning
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - transform.position;
        angle = Vector2.SignedAngle(Vector2.right, direction)-90;
        transform.eulerAngles = new Vector3(0, 0, angle);
        //flashlight
        if (batteryBar.slider.value > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                flashlight.SetActive(!flashlight.gameObject.activeSelf);
                flashlightOn = !flashlightOn;
            }
            //decrease battery
            if (flashlightOn)
            {
                batteryBar.DecreaseBattery(batDecreaseRate);
            }
        } else
        {
            flashlight.SetActive(false); flashlightOn = false;
        }
        //score
        scoreText.text = "Score: " + (room + 1);
    }

    private void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Recharge")
        {
            audioSource.clip = charge;
            audioSource.Play();
            batteryBar.IncreaseBattery(30);
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Kill")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            room = 0;
            print("deadadddd");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Key")
        {
            hasKey = true;
            audioSource.clip = getKey;
            Destroy(collision.gameObject);
            audioSource.Play();
        }
        if (collision.gameObject.tag == "Center" && collision.gameObject.GetComponent<DoorCenterScript>().able_to_unlock && hasKey)
        {
            hasKey = false;
            collision.gameObject.SetActive(false);
            audioSource.clip = openDoor;
            audioSource.Play();
            room++;
            GameObject newGhost = Instantiate(ghostPrefab, Vector2.zero, Quaternion.identity);
            newGhost.GetComponent<GhostScript>().player = gameObject.transform;
            int i = 0;
            while (newGhost.GetComponent<BoxCollider2D>().IsTouching(GetComponent<BoxCollider2D>()) || i < 100)
            {
                i++;
                newGhost.transform.position = transform.position;
                newGhost.transform.position += new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
            }
            roomGen.GenerateRoom();
        }
    }
}
