using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject obj;
    float timer = 0;
    public float gapSecond = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > gapSecond)
        {
            GameObject newObj = Instantiate(obj);
            newObj.transform.position = new Vector3(Random.Range(0, 5), Random.Range(0, 5), 0);
            timer = 0;
            Destroy(newObj, 15);
        }

    }
}
