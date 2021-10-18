using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Scoring : IComponentData
{
    public int scoreValue;    
    
}
