using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class Spawner : MonoBehaviour
{

    public GameObject obj;
    public GameObject attackEffect;
    float timer = 0;
    public float gapSecond = 3;
    
    List<GameObject> mobs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        timer += Time.deltaTime;

        if (timer > gapSecond)
        {
            Vector3 position = new Vector3(Random.Range(0, 8), Random.Range(-5, 2), 0);
            GameObject newObj = PhotonNetwork.Instantiate("dongle", position, Quaternion.identity, 0);
            mobs.Add(newObj);
            timer = 0;
        
        }
    }

   
}
