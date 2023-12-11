using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public GameObject bullet;
    public MovePlayer mp;
    public Rigidbody2D myRigid;
    public int speed;
    public ParticleSystem deathParts;
    public ParticleSystem breakParts;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        myRigid.velocity = transform.right * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BreakWall"))
        {
            

            if (collision.gameObject.CompareTag("Enemy"))
            {
                deathParts.transform.position = collision.transform.position;
                deathParts.Play();
            }

            else if (collision.gameObject.CompareTag("BreakWall"))
            {
                breakParts.transform.position = this.transform.position;
                breakParts.Play();
            }

            Destroy(collision.gameObject);
        }
    }
}
