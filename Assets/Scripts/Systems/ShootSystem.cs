using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ShootSystem : SystemBase
{
    protected override void OnUpdate()
    {
        
        
        
        Entities.ForEach((in Shooter shooter,in Translation translation, in Rotation rotation) => 
        {
            if(Input.GetButton("Fire1"))
            {
                Entity laserBeam = EntityManager.Instantiate(shooter.laserPrefab1);
                EntityManager.SetComponentData(laserBeam, new Translation { Value = translation.Value });
                EntityManager.SetComponentData(laserBeam, new Rotation { Value = math.mul(rotation.Value.value, quaternion.RotateY(90f)) });
                //EntityManager.SetComponentData(laserBeam, new Movable { direction = (translation.Value + new float3(0, 0, 1)), speed = 0.5f });
            }
            
        }).WithStructuralChanges().Run();
    }
}
