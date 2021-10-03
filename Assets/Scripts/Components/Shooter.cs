using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Shooter : IComponentData
{
    public Entity laserPrefab1;
    public float timeDelayShoot;
    public float laserSpeed;

}
