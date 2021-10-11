using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct AsteroidManager : IComponentData
{
    public Entity asteroidPrefab3;
    public float minScaleFactorAsteroid1, maxScaleFactorAsteroid1;


}
