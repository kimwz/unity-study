using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Photon.Pun;
using UnityEngine.TextCore.Text;

public class AttackableUnit : MonoBehaviourPunCallbacks
{
    public int hp = 20;
    SkeletonAnimation anim;

    // Start is called before the first frame update
    private void Start()
    {
        //attackedEffect = Resources.Load<GameObject>("path");
        anim = GetComponent<SkeletonAnimation>();
        anim.AnimationState.SetAnimation(0, "Casting", false);
        anim.AnimationState.AddAnimation(0, "Idle", true, 0);
    }
    public void Attacked()
    {
            //RPCAttacked();
        photonView.RPC("RPCAttacked", RpcTarget.All);
        
    }

    [PunRPC]
    public void RPCAttacked()
    {
        hp -= 10;

        anim.AnimationState.SetAnimation(0, "Idle", true);

        StartCoroutine(AttackEffect());
        if (hp <= 0)
        {
            anim.AnimationState.AddAnimation(0, "Die", false, 0.2f);
            StartCoroutine(DestroySelf());
        }
        else
        {
            anim.AnimationState.AddAnimation(0, "Hit", false, 0.2f);
            anim.AnimationState.AddAnimation(0, "Idle", true, 0);
        }
    }

    public bool isDead()
    {
        return hp <= 0;
    }

    IEnumerator AttackEffect()
    {
        yield return new WaitForSeconds(0.2f);
        var effect = AttackedEffectPool.GetObject();
        effect.transform.position = transform.position;
        StartCoroutine(AttackedEffectPool.ReturnObject(effect, 0.1f));
    }

    IEnumerator DestroySelf()
    {
        Score.score += 10;
        yield return new WaitForSeconds(0.4f);
        //Destroy(attackedEffect);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        //Destroy(gameObject);
    }

}
