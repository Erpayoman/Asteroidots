using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ShootSystem : SystemBase
{
    float shootTime = 0f;

    protected override void OnUpdate()
    {

        float dt = Time.DeltaTime;       
       
        

        Entities.ForEach((in Shooter shooter,in Translation translation, in Rotation rotation) => 
        {
            shootTime += dt;
                       
            if(shootTime >= shooter.timeDelayShoot)
            {
                if (Input.GetButton("Fire1"))
                {
                    Entity laserBeam = EntityManager.Instantiate(shooter.laserPrefab1);
                    EntityManager.SetComponentData(laserBeam, new Translation { Value = translation.Value });
                    EntityManager.SetComponentData(laserBeam, new Rotation { Value = rotation.Value });
                    EntityManager.SetComponentData(laserBeam, new Laser { speed = shooter.laserSpeed });

                }
                shootTime = 0f;
            }
            

        }).WithStructuralChanges().Run();
    }
    
}
