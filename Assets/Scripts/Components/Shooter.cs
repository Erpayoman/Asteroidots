using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Shooter : IComponentData
{
    public float timeDelayShoot;
    public float laserSpeed;

}
