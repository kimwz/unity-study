using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class AttackableUnit : MonoBehaviour
{
    public GameObject attackedEffect;
    public int hp = 20;
    SkeletonAnimation anim;

    
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<SkeletonAnimation>();
        anim.AnimationState.SetAnimation(0, "Casting", false);
        anim.AnimationState.AddAnimation(0, "Idle", true, 0);
    }
    public void Attacked()
    {
        Debug.Log("Attacked");
        hp -= 10;

        anim.AnimationState.SetAnimation(0, "Idle", true);

        StartCoroutine(AttackEffect());
        if (hp <= 0)
        {
            anim.AnimationState.AddAnimation(0, "Die", false, 0.2f);
            StartCoroutine(DestroySelf());
        } else
        {
            anim.AnimationState.AddAnimation(0, "Hit", false, 0.2f);
            anim.AnimationState.AddAnimation(0, "Idle", true, 0);
        }
    }

    IEnumerator AttackEffect()
    {
        yield return new WaitForSeconds(0.2f);
        if (attackedEffect != null)
        {
            attackedEffect.transform.position = transform.position;
            attackedEffect.SetActive(false);
            attackedEffect.SetActive(true);
        }
    }

    IEnumerator DestroySelf()
    {
        Score.score += 10;
        yield return new WaitForSeconds(0.4f);
        Destroy(attackedEffect);
        Destroy(gameObject);
    }
}
