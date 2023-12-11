using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string whereToGo;
    public GameObject spawnpoint;
    public GameObject respawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gob = collision.gameObject;

        if (gob.tag == "Player")
        {
            SceneManager.LoadScene(whereToGo);
            gob.transform.position = spawnpoint.transform.position;
            respawn.transform.position = spawnpoint.transform.position;
        }
    }
}
