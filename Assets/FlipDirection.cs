using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipDirection : MonoBehaviour
{
    Vector3 lastPosition;
    int direction;
    float threshold = 0.2f;
    void setDirection(int _direction)
    {
        if (direction != _direction)
        {
            if (threshold <= 0)
            {
                transform.rotation = Quaternion.Euler(0, _direction, 0);
                direction = _direction;
                threshold = 0.2f;
            }
            else
            {
                threshold -= Time.deltaTime;
            }
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        setDirection(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPosition.x < transform.position.x)
        {
            setDirection(180);
        }
        else if (lastPosition.x > transform.position.x)
        {
            setDirection(0);
        }

        lastPosition = transform.position;
        
    }
}
