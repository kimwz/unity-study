using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using CodeMonkey.HealthSystemCM;
using UnityEngine.SceneManagement;

public class CharacterMove : MonoBehaviour, IGetHealthSystem
{
    public GameObject playerBox;
    public GameObject manaManager;
    public GameObject criticalAttackEffect;
    public GameObject stageManager;
    
    private HealthSystem healthSystem;
    SkeletonAnimation skeletonAnimation;
    public string curAnimation = "Idle";
    bool attacking = false;
    bool hitting = false;

    private void Awake()
    {
        healthSystem = new HealthSystem(100);
        healthSystem.OnDead += HealthSystem_OnDead;
        criticalAttackEffect = Instantiate(criticalAttackEffect);
        criticalAttackEffect.SetActive(false);
        stageManager.GetComponent<StageManager>().Restart();
    }

    List<AttackableUnit> targets = new List<AttackableUnit>();
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
            transform.rotation = Quaternion.Euler(0, _direction, 0);
            direction = _direction;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Players.players.Add(gameObject);
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!attacking)
            {
                attacking = true;

                int maximumOnce = 1;
                if (manaManager.GetComponent<ManaManager>().GetHealthSystem().GetHealth() == 100)
                {
                    StartCoroutine(AttackEffect());
                    setAnimation("CriticalAttack", false);
                    manaManager.GetComponent<ManaManager>().Damage();
                    maximumOnce = 100;
                } else
                {
                    setAnimation("Attack", false);
                }
                
                if (targets.Count > 0)
                {
                    foreach (AttackableUnit target in targets)
                    {
                        if (target)
                        {
                            target.Attacked();
                            maximumOnce--;
                            if (maximumOnce <= 0) break;
                        }
                    }
                }

                StartCoroutine(didAttack());
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                setDirection(0);
                playerBox.transform.Translate(Vector3.right * Time.deltaTime * 3);
                setAnimation("Run");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                setDirection(180);
                playerBox.transform.Translate(Vector3.left * Time.deltaTime * 3);
                setAnimation("Run");
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerBox.transform.Translate(Vector3.up * Time.deltaTime * 3);
                setAnimation("Run");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                playerBox.transform.Translate(Vector3.down * Time.deltaTime * 3);
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
        if (criticalAttackEffect != null)
        {
            criticalAttackEffect.transform.position = playerBox.transform.position;
            //criticalAttackEffect.transform.Translate(-Vector3.down * 7);
            criticalAttackEffect.SetActive(false);
            criticalAttackEffect.SetActive(true);
        }
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
        hitting= false;
        setAnimation("Hit", false);
        Damage();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        targets.Add(collision.gameObject.GetComponent<AttackableUnit>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        targets.Remove(collision.gameObject.GetComponent<AttackableUnit>());
    }

    public void Restart()
    {
        healthSystem.HealComplete();
    }


    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        stageManager.GetComponent<StageManager>().GameOver();
    }

    public void Damage()
    {
        healthSystem.Damage(5);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

}
