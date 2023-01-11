using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class CharacterMove : MonoBehaviour
{
    SkeletonAnimation skeletonAnimation;
    public string curAnimation = "Idle";

    List<AttackableUnit> targets = new List<AttackableUnit>();
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
            if (targets.Count > 0)
            {
                foreach(AttackableUnit target in targets)
                {
                    target.Attacked();
                }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targets.Add(collision.gameObject.GetComponent<AttackableUnit>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        targets.Remove(collision.gameObject.GetComponent<AttackableUnit>());
        
    }

}
