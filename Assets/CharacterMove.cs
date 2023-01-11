using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterMove : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    public string curAnimation = "Idle";

    SkeletonAnimation target = null;
    void setAnimation(string name, bool loop = true)
    {
        if (curAnimation != name)
        {
            curAnimation = name;
            skeletonAnimation.AnimationState.SetAnimation(0, curAnimation, loop);
        }
    }

    int direction = 0;
    void setDirection(int _direction)
    {
        if (direction != _direction)
        {
            transform.rotation = Quaternion.Euler(0, _direction, 0);
            direction = _direction;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            setAnimation("Attack", false);
            skeletonAnimation.AnimationState.AddAnimation(0, "Idle", true, 0);
            curAnimation = "Idle";
            if (target != null)
            {
                target.AnimationState.SetAnimation(0, "Idle", true);
                target.AnimationState.AddAnimation(0, "Hit", false, 0.2f);
                target.AnimationState.AddAnimation(0, "Idle", true, 0);
            }
         }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                setDirection(0);
                transform.Translate(Vector3.right * Time.deltaTime * 3);
                setAnimation("Run");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                setDirection(180);
                transform.Translate(Vector3.right * Time.deltaTime * 3);
                setAnimation("Run");
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * Time.deltaTime * 3);
                setAnimation("Run");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * Time.deltaTime * 3);
                setAnimation("Run");
            }

            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                setAnimation("Idle");
            }
        }
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        target = collision.gameObject.GetComponent<SkeletonAnimation>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        target = null;
    }
}
