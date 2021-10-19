using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PrefabManagerECS : IComponentData
{
    public Entity shipPrefab;
    public Entity laser;
    
    
}
