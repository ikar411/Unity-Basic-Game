using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Playermovement : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject player;
    public float forwardforce = 1000f;
    public float sidewaysforce = 500f;
    public float speed = 10f ;
    public Text scorenum;

    private bool moveright=false;
    private bool moveleft=false;
    private int score;
    private Vector2 dir;
    private Vector3 touchposition;
    private Vector3 touchdirection;

    private void Start()
    {
        score = 0;
    }

    public void move(Vector2 direction)
    {
        //Vector3 vas = new Vector3(direction.x, 0, 0);
        rb.velocity = new Vector2 (direction.x, 0) * speed ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardforce * Time.deltaTime);

        if (moveright)
        {
            rb.AddForce(sidewaysforce * Time.deltaTime,0,0);

        }
        if (moveleft)
        {
            rb.AddForce(-sidewaysforce * Time.deltaTime, 0, 0);

        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    void Update()
    {
        if (player.transform.position.y < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        //PC GAME
        if (Input.GetKeyDown("right"))
        {
            moveright = true;
        }
        if (Input.GetKeyUp("right"))
        {
            moveright = false;
        }
        if (Input.GetKeyDown("left"))
        {
            moveleft = true;
        }
        if (Input.GetKeyUp("left"))
        {
            moveleft = false;
        }

        //Mobile Game

        /*if (Input.touches[0].phase == TouchPhase.Moved)
        {
            dir = Input.touches[0].deltaPosition.normalized;
            //speed = Input.touches[0].deltaPosition.magnitude / Input.touches[0].deltaTime;
            if (dir.x > 0) { moveright = true; moveleft = false; }
             else if (dir.x < 0) { moveleft = true; moveright = false; }
             else{ moveright = false; moveleft = false; }
            
            move(dir);
        }
        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
          }  */
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                // get the touch position from the screen touch to world point
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, 0));
                // lerp and set the position of the current object to that of the touch, but smoothly over time.
                transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
            }
        }

    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("GameOver");
   
        }
        
    }
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("level"))
        {
            collision.gameObject.SetActive(false);
            score += 10;
            scorenum.text = score.ToString();
        }
        if (collision.CompareTag("coin"))
        {
            collision.gameObject.SetActive(false);
            score += 3;
            scorenum.text = score.ToString();
        }
    }
   */
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("power"))
        {
            other.gameObject.SetActive(false);
            score++;
            scorenum.text = score.ToString();
        }
        if (other.CompareTag("level"))
        {
            other.gameObject.SetActive(false);
            score += 10;
            scorenum.text = score.ToString();
            SceneManager.LoadScene("GameOver");
        }

    }
   

    public void pausegame()
    {
        Time.timeScale = 0;
    }

    public void playgame()
    {
        Time.timeScale = 1;
    }
}
