using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovePlayer : MonoBehaviour
{
    public bool isGrounded = true;
    bool hasDoubleJump = false;
    bool hasGroundPound = false;
    bool hasBullet = false;
    public GameObject bullet;
    bool jumpPress = false;
    public bool pounding = false;
    public int jumpsLeft = 1;
    public string up;
    public string down;
    public string left;
    public string right;
    public string shoot;
    public string groundPound;
    public Rigidbody2D myRigid;
    public int speed;
    public float lastFired;
    public GameObject respawn;
    public float yVel;
    public float xVel;
    public ParticleSystem deathParts;
    public int life = 3;
    public TMP_Text lifeDisplay;
    public ParticleSystem jumpParts;
    public ParticleSystem breakParts;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = this.GetComponent<Rigidbody2D>();
        deathParts = GameObject.Find("Blood").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeDisplay.text = life.ToString();

        if (Input.GetKeyDown(up) && jumpsLeft > 0)
        {
            Jump();
            isGrounded = false;
            jumpPress = true;
        }

        if (Input.GetKeyUp(up))
        {
            jumpPress = false;
        }

        if (Input.GetKey(groundPound) && hasGroundPound == true && pounding == false && isGrounded == false)
        {
            GroundPound();
            pounding = true;
        }

        if (Input.GetKey(shoot) && hasBullet == true)
        {
            Shoot();
        }

        yVel = myRigid.velocity.y;
        xVel = myRigid.velocity.x;

    }

    void FixedUpdate()
    {
        if (Input.GetKey(left))
        {
            //myRigid.AddForce(-this.transform.right * speed, ForceMode2D.Impulse);
            myRigid.velocity = new Vector3(-speed, yVel, 0f);
            //this.transform.Rotate(new Vector3(-speed, myRigid.velocity.y, 0f));
        }

        if (Input.GetKey(right))
        {
            //myRigid.AddForce(this.transform.right * speed, ForceMode2D.Impulse);
            myRigid.velocity = new Vector3(speed, yVel, 0f);
            //this.transform.Rotate(new Vector3(speed, myRigid.velocity.y, 0f));
        }

        if (life == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            Destroy(this.gameObject);
            Destroy(lifeDisplay);
        }

    }

    void Jump()
    {
        Debug.Log("Jump");
        myRigid.AddForce(this.transform.up * 10, ForceMode2D.Impulse);
        jumpsLeft -=1;
        jumpParts.transform.position = myRigid.transform.position;
        jumpParts.Play();
    }

    void GroundPound()
    {
        Debug.Log("Ground Pound");
        myRigid.AddForce(this.transform.up * -20, ForceMode2D.Impulse);
    }

    void Shoot()
    {
        if (Time.time > lastFired + 0.5)
        {
            Debug.Log("Shoot");
            Instantiate(bullet, transform.position + transform.right * 1, transform.rotation);
            lastFired = Time.time;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("BreakGround") && pounding == false)
        {
            isGrounded = true;
            pounding = false;

            if (hasDoubleJump == true)
            {
                jumpsLeft = 2;
            }
            else
            {
                jumpsLeft = 1;
            }
        }

        if (collision.gameObject.CompareTag("Enemy") && pounding == true || collision.gameObject.CompareTag("BreakGround") && pounding == true)
        {
            Destroy(collision.gameObject);

            if (collision.gameObject.CompareTag("Enemy"))
            {
                deathParts.transform.position = collision.transform.position;
                deathParts.Play();
            }

            else if (collision.gameObject.CompareTag("BreakGround"))
            {
                breakParts.transform.position = myRigid.transform.position;
                breakParts.Play();
            }
        }

        if (collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Enemy") && pounding == false)
        {
            Debug.Log("Touching Spikes");
            deathParts.transform.position = myRigid.transform.position;
            deathParts.Play();
            myRigid.transform.position = respawn.transform.position;
            life -= 1;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Ground") && jumpPress == false)
        {
            isGrounded = false;
            jumpsLeft -= 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("2Jump"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Has Double Jump");
            hasDoubleJump = true;
            jumpsLeft += 1;
        }

        else if (collision.gameObject.CompareTag("GPound"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Has Ground Pound");
            hasGroundPound = true;
        }

        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Has Bullet");
            hasBullet = true;
        }

        else if (collision.gameObject.CompareTag("End"))
        {
            Destroy(this.gameObject);
            Destroy(this.lifeDisplay);
        }
    }
}
