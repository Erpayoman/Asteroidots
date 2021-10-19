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
    float fxVolumen = 0.007f;
    PrefabManagerECS prefabManager;

    protected override void OnUpdate()
    {

        float dt = Time.DeltaTime;
        Entities.ForEach((ref PrefabManagerECS prefabManagerECS) =>
            {
                this.prefabManager = prefabManagerECS;

            }).WithStructuralChanges().WithoutBurst().Run();
        
        
         Entities.ForEach((in Shooter shooter, in Translation translation, in Rotation rotation) =>
         {
             shootTime += dt;



             if (shootTime >= shooter.timeDelayShoot)
             {
                 if (Input.GetButton("Fire1"))
                 {
                     Entity laserBeam = EntityManager.Instantiate(prefabManager.laser);
                     EntityManager.SetComponentData(laserBeam, new Translation { Value = translation.Value });
                     EntityManager.SetComponentData(laserBeam, new Rotation { Value = rotation.Value });
                     EntityManager.SetComponentData(laserBeam, new Laser { speed = shooter.laserSpeed });

                     AudioManager.instance.PlayFX("laser1", fxVolumen);

                 }
                 shootTime = 0f;
             }


         }).WithStructuralChanges().Run();

        
    }
        

        
        

        
    
}
