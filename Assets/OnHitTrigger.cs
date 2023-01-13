using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitTrigger : MonoBehaviour
{
    public GameObject triggerTarget;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerTarget.GetComponent<CharacterMove>().OnHit();
    }
}
