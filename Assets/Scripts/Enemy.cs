using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public MovePlayer mp;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        mp = Player.GetComponent<MovePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (mp.pounding == true)
            {
                Destroy(this);
            }
        }
    }
}
