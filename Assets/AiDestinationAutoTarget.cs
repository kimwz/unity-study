using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Photon.Pun;

public class AiDestinationAutoTarget : MonoBehaviour
{
    AIDestinationSetter setter;
    // Start is called before the first frame update
    void Start()
    {
        setter = GetComponent<AIDestinationSetter>();
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (setter.target == null && Players.players.Count > 0)
        {
            setter.target = Players.players[0].transform;
        }
    }
}
