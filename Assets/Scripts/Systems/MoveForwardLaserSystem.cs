using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MoveForwardLaserSystem : SystemBase
{
    protected override void OnUpdate()
    {

        float dt = Time.DeltaTime;

        Entities.ForEach((ref Translation t, in LocalToWorld ltw,in Laser laser) =>
        {
            
           t.Value += ltw.Forward * dt*laser.speed;

        }).Schedule();
    }
}