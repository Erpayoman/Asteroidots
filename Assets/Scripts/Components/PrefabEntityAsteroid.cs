using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;


[GenerateAuthoringComponent]
public struct PrefabEntityAsteroid : IComponentData
{
    public Entity asteroidPrefab;
    
    
}
