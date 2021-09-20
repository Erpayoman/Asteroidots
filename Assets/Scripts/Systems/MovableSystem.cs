using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

public class MovableSystem : SystemBase
{
    protected override void OnUpdate()
    {

        /* float dt = Time.DeltaTime;


        Entities.ForEach((ref Movable mov, ref Translation translation, ref Rotation rotation, in Player player) => 
         {
             translation.Value += mov.speed * mov.direction * dt;
             //rotation.Value = math.mul(rotation.Value.value, quaternion.RotateY(mov.speed*dt));
         }).Schedule();*/

        Entities.ForEach((ref PhysicsVelocity vel, in Movable mov) =>
        {
            var step = mov.direction * mov.speed;
            vel.Linear = step;
        }).Schedule();
    }
}
