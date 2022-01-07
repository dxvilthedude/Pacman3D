using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Boost
{
    float rotationSpeed = 20f;
    protected override void Start()
    {
        base.Start();
        boostName = "SPEEDBOOST";
        boostManager.SpawnInfo(boostName);
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    public override void BoostEffect() => boostManager.SpeedBoost();
}
