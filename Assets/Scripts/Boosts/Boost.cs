using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boost : Collectable
{
    public BoostManager boostManager;
    public string boostName;
    protected override void Start()
    {
        base.Start();
        boostManager = FindObjectOfType<BoostManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            boostManager.BoostUI.text = boostName + " :";
            BoostEffect();
            boostManager.BoostOnMap = false;
            Destroy(this.gameObject);
        }
    }
    public abstract void BoostEffect();    
}
