using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<AttackableUnit> targets = new List<AttackableUnit>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        targets.Add(collision.gameObject.GetComponent<AttackableUnit>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targets.Remove(collision.gameObject.GetComponent<AttackableUnit>());
    }

    private void removeIsDead()
    {
        var removeTargets = new List<AttackableUnit>();
        foreach (var target in targets)
        {
            if (target == null || target.isDead())
            {
                removeTargets.Add(target);
            }
        }

        foreach (var target in removeTargets)
        {
            targets.Remove(target);
        }
    }

    public void attackOne()
    {
        removeIsDead();
        if (targets.Count > 0)
        {
            targets[0].Attacked();
        }
    }

    public void attackAll()
    {
        removeIsDead();
        foreach (var target in targets)
        {
            target.Attacked();
        }

    }
}
