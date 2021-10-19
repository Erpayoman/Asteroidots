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

        if(prefabManager.Equals(null))
        {
            Entities.ForEach((ref PrefabManagerECS prefabMng, in Entity e) =>
            {
                prefabManager = prefabMng;

            }).WithStructuralChanges().WithoutBurst().Run();
        }
        else
        {
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
        

        
    
}
