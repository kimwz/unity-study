using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject obj;
    public GameObject attackEffect;
    float timer = 0;
    public float gapSecond = 3;
    bool stop;
    List<GameObject> mobs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > gapSecond && !stop)
        {
            GameObject newObj = Instantiate(obj);
            newObj.transform.position = new Vector3(Random.Range(0, 8), Random.Range(-5, 2), 0);
            newObj.GetComponent<AttackableUnit>().attackedEffect = Instantiate(attackEffect);
            mobs.Add(newObj);
            timer = 0;
        }
    }

    public void GameOver()
    {
        stop = true;
        foreach(GameObject mob in mobs)
        {
            if (mob)
            {
                Destroy(mob);
            }
        }
    }

    public void Restart()
    {
        stop = false;
    }
}
