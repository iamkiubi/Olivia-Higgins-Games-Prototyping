using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject respawn;
    public ParticleSystem blood;
    public ParticleSystem breakParts;
    public ParticleSystem jumpParts;
    public Canvas UI;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.prefab);
        DontDestroyOnLoad(this.respawn);
        DontDestroyOnLoad(this.blood);
        DontDestroyOnLoad(this.breakParts);
        DontDestroyOnLoad(this.jumpParts);
        DontDestroyOnLoad(this.UI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
