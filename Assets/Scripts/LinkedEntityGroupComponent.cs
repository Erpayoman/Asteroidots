using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class LinkedEntityGroupComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var buffer = dstManager.AddBuffer<LinkedEntityGroup>(entity);

        var children = transform.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            var childEntity = conversionSystem.GetPrimaryEntity(child.gameObject);
            buffer.Add(childEntity);
        }
    }
}