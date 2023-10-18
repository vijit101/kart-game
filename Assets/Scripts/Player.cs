using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Stone")
        {
            playerForwardSpeed = 0; // velocity becomes 0 
            timeLapsed = 0;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Boost")
        {
            playerForwardSpeed =playerForwardSpeed+ 2;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Goal")
        {
            TimeLeftText.text = "You Win";
            Debug.Log("You Win");
            // Load new level
        }
    }
    public float playerForwardSpeed = 1, horizontalCarSpeed, maxAccelerationTime , LevelTime;
    Rigidbody2D playerRGBD;
    float timeLapsed = 0 , horizontalInput,yOffset;
    public Camera myCam;
    public TextMeshProUGUI TimeLeftText;
    // Start is called before the first frame update
    void Start()
    {
        playerRGBD = GetComponent<Rigidbody2D>();
        yOffset = myCam.transform.position.y - transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        playerRGBD.velocity = new Vector2(0, 1 * playerForwardSpeed);
        horizontalInput = Input.GetAxis("Horizontal");
        LevelTime -= Time.deltaTime;
        TimeLeftText.text = "Time Left :" + (int)LevelTime + "s";
        if (timeLapsed < maxAccelerationTime)
        {
            playerForwardSpeed += Time.fixedDeltaTime;//gradually increasing speed of car 
            timeLapsed += Time.deltaTime;

        }
        if (LevelTime < 0)
        {
            Time.timeScale = 0;
            TimeLeftText.text = "You Lose";
        }
        if (horizontalInput > 0 || horizontalInput < 0)
        {
            playerRGBD.AddForce(new Vector2(horizontalInput * horizontalCarSpeed, 0));           
        }

        myCam.transform.position = new Vector3(0, transform.position.y+ yOffset, -10);
    }

    private void FixedUpdate()
    {
        
    }
}
