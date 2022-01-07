using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionBoost : Boost
{
    float rotationSpeed = 20f;

    protected override void Start()
    {
        base.Start();
        boostName = "VISIONBOOST";
        boostManager.SpawnInfo(boostName);
    }
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
    public override void BoostEffect() => boostManager.VisionBoost();
}
