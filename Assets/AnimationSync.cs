using UnityEngine;
using Photon.Pun;
using Spine;
using Spine.Unity;

public class AnimationSync : MonoBehaviourPun, IPunObservable
{
    private SkeletonAnimation skeletonAnimation;
    private GameObject character;

    private void Start()
    {
        character = transform.Find("Character").gameObject;
        skeletonAnimation = character.GetComponent<SkeletonAnimation>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!skeletonAnimation) return;
        if (stream.IsWriting)
        {
            // 스트림에 SkeletonAnimation 상태를 쓴다.
            stream.SendNext(skeletonAnimation.AnimationName);
            stream.SendNext(skeletonAnimation.loop);
            stream.SendNext(character.transform.rotation);
        }
        else
        {
            // 스트림에서 SkeletonAnimation 상태를 읽는다.
            skeletonAnimation.AnimationName = (string)stream.ReceiveNext();
            skeletonAnimation.loop = (bool)stream.ReceiveNext();
            character.transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}