using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstantiatePlayer : MonoBehaviourPunCallbacks
{
    public GameObject Camera;
    public GameObject StageManager;

    override public void OnJoinedRoom()
    {
        CreatePlayerObject();
    }

    void CreatePlayerObject()
    {
        Vector3 position = new Vector3(0, 0, 0);

        GameObject newPlayerObject = PhotonNetwork.Instantiate("Player", position, Quaternion.identity, 0);
        //StageManager.GetComponent<StageManager>().player = newPlayerObject.transform.Find("Player").gameObject;
        //newPlayerObject.GetComponentInChildren<CharacterMove>().stageManager = StageManager;
        //newPlayerObject.GetComponentInChildren<CharacterMove>().main = true;
        Camera.GetComponent<CameraFollow>().InitTarget(newPlayerObject.transform);
    }
}
