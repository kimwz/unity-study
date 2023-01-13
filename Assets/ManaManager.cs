using System.Collections;
using System.Collections.Generic;
using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class ManaManager : MonoBehaviour, IGetHealthSystem
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = new HealthSystem(100);
        healthSystem.Damage(50);
        healthSystem.OnDead += HealthSystem_OnDead;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fill(Time.deltaTime * 30);
    }


    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {

    }

    public void Fill(float amount)
    {
        healthSystem.Heal(amount);
    }

    public void Damage()
    {
        healthSystem.Damage(100);
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
