using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PrefabManagerECS : IComponentData
{
    public Entity shipPrefab;
    public Entity laser;
    public Entity playerExplosion,asteroidExplosion;
    public Entity asteroidPrefab1, asteroidPrefab2, asteroidPrefab3;
    
    
}
