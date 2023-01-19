using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using CodeMonkey.HealthSystemCM;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public GameObject character;
    public GameObject healthManager;
    public GameObject manaManager;
    public GameObject attackManager;
    public GameObject stageManager;
    
    SkeletonAnimation skeletonAnimation;

    public string curAnimation = "Idle";
    bool attacking = false;
    bool hitting = false;

    private void Awake()
    {
    }

    
    void setAnimation(string name, bool loop = true)
    {
        if (curAnimation != name)
        {
            curAnimation = name;
            skeletonAnimation.AnimationState.SetAnimation(0, curAnimation, loop);
            if (!loop)
            {
                skeletonAnimation.AnimationState.AddAnimation(0, "Idle", true, 0);
                curAnimation = "Idle";
            }
        }
    }

    int direction = 0;
    void setDirection(int _direction)
    {
        if (direction != _direction)
        {
            transform.Find("Character").rotation = Quaternion.Euler(0, _direction, 0);
            direction = _direction;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Players.players.Add(gameObject);
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        if (Input.GetKey(KeyCode.Space))
        {
            if (!attacking)
            {
                attacking = true;

                if (manaManager.GetComponent<ManaManager>().GetHealthSystem().GetHealth() == 100)
                {
                    StartCoroutine(AttackEffect());
                    setAnimation("CriticalAttack", false);
                    manaManager.GetComponent<ManaManager>().Damage();
                    attackManager.GetComponent<AttackManager>().attackAll();
                }
                else
                {
                    setAnimation("Attack", false);
                    attackManager.GetComponent<AttackManager>().attackOne();
                }
                StartCoroutine(didAttack());
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                setDirection(0);
                //transform.Translate(Vector3.right * Time.deltaTime * 3);
                GetComponent<CharacterController>().Move(Vector3.right * Time.deltaTime * 3);
                setAnimation("Run");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                setDirection(180);
                //transform.Translate(Vector3.left * Time.deltaTime * 3);
                GetComponent<CharacterController>().Move(Vector3.left * Time.deltaTime * 3);
                setAnimation("Run");
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                //transform.Translate(Vector3.up * Time.deltaTime * 3);
                GetComponent<CharacterController>().Move(Vector3.up * Time.deltaTime * 3);
                setAnimation("Run");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                //transform.Translate(Vector3.down * Time.deltaTime * 3);
                GetComponent<CharacterController>().Move(Vector3.down * Time.deltaTime * 3);
                setAnimation("Run");
            }

            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                setAnimation("Idle");
            }

        }
    }

    IEnumerator AttackEffect()
    {
        yield return new WaitForSeconds(0.2f);
        //if (criticalAttackEffect != null)
        //{
        //    criticalAttackEffect.transform.position = playerBox.transform.position;
        //    criticalAttackEffect.SetActive(false);
        //    criticalAttackEffect.SetActive(true);
        //}
    }

    IEnumerator didAttack()
    {
        yield return new WaitForSeconds(0.3f);
        attacking = false;
    }

    public void OnHit()
    {
        if (!hitting && !attacking)
        {
            hitting = true;
            StartCoroutine(didHit());
        }
    }

    IEnumerator didHit()
    {
        yield return new WaitForSeconds(0.2f);
        hitting = false;
        setAnimation("Hit", false);
        healthManager.GetComponent<HealthManager>().Damage();
    }

    public void Restart()
    {
        healthManager.GetComponent<HealthManager>().GetHealthSystem().HealComplete();
    }

    public void OnDead()
    {
        if (photonView.IsMine)
        {
            stageManager.GetComponent<StageManager>().GameOver();
        }
    }
}
