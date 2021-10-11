using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct PrefabAsteroidsEntities : IComponentData
{
    public Entity asteroidPrefab;
    public float minScaleFactorAsteroid1, maxScaleFactorAsteroid1;


}
