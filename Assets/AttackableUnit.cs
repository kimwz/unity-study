using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackableUnit : MonoBehaviour
{
    public int hp = 20;
    SkeletonAnimation anim;
    // Start is called before the first frame update

    private void Start()
    {
        anim = GetComponent<SkeletonAnimation>();
    }
    public void Attacked()
    {
        hp -= 10;

        anim.AnimationState.SetAnimation(0, "Idle", true);
        

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

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
}
