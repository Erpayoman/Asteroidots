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

        RaycastHit hit = new RaycastHit();
        float dt = Time.DeltaTime;

        Entities.ForEach((in Shooter shooter,in Translation translation, in Rotation rotation,in LocalToWorld ltw) => 
        {
            if(Input.GetButton("Fire1"))
            {
                Entity laserBeam = EntityManager.Instantiate(shooter.laserPrefab1);
                EntityManager.SetComponentData(laserBeam, new Translation { Value = translation.Value });
                EntityManager.SetComponentData(laserBeam, new Rotation { Value = rotation.Value });

                /*Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50);
                Vector3 hitPosVector = hit.point;
                //EntityManager.SetComponentData(laserBeam, new Movable { direction = math.normalize((translation.Value + new float3(hitPosVector.x, 0, hitPosVector.z))- translation.Value), speed = 2f });
                EntityManager.SetComponentData(laserBeam, new Movable { direction = math.normalize(ltw.Forward+translation.Value) - translation.Value, speed = 2f });*/



            }

        }).WithStructuralChanges().Run();
    }
    
}
