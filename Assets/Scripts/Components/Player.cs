using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Player : IComponentData
{
    
    public float newX, newY;
    public float aceleration;
    public Entity explosionPrefab;
    

}
