using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class PlayerSystem : SystemBase
{
    
    
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;

        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        //Debug.Log("X: " + x);
       // Debug.Log("Y: " + y);

        

        RaycastHit hit = new RaycastHit();
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50);
        Vector3 hitPosVector = hit.point;

        Entities.ForEach((ref Movable mov, ref Translation translation, ref Rotation rotation, ref Player player) => 
        {

            //Player movement 

            //mov.direction = new float3(x, 0, y);
           
            if (x > 0) player.newX = math.clamp(player.newX + (dt * player.aceleration), 0f, 5f);

            if (x < 0) player.newX = math.clamp(player.newX - (dt * player.aceleration), -5f,0f);

            if (y < 0) player.newY = math.clamp(player.newY - (dt * player.aceleration), -5f, 0f );

            if (y > 0) player.newY = math.clamp(player.newY + (dt * player.aceleration), 0f, 5f);
           
            mov.direction = new float3(player.newX, 0, player.newY);

            //Transportation on the edge of the screen 16:9
            if (translation.Value.x < -4.6f) 
                translation.Value = new float3(4.6f, translation.Value.y, translation.Value.z);
            if(translation.Value.x > 4.6f)
                translation.Value =  new float3(-4.6f, translation.Value.y, translation.Value.z);

            if (translation.Value.z < -2.8f)
                translation.Value =  new float3(translation.Value.x, translation.Value.y, 2.8f);
            if (translation.Value.z > 2.8f)
                translation.Value = new float3(translation.Value.x, translation.Value.y, -2.8f);

            //Rotation base on mouse position and raycast against the background.

            float3 hitPos = new float3(hitPosVector.x, 1f, hitPosVector.z);
            float3 relativePos = hitPos - translation.Value;

            // the second argument, upwards, defaults to Vector3.up

            quaternion start = rotation.Value;
            quaternion end = quaternion.LookRotation(relativePos, math.up());

            //quaternion result=quaternion.sl
            rotation.Value = end;



        }).Schedule();

        Entities.ForEach((in Player player, in Kill kill,in Translation translation) =>
        {
            Entity explosion = EntityManager.Instantiate(player.explosionPrefab);
            EntityManager.SetComponentData(explosion, new Translation { Value = translation.Value });

        }).WithStructuralChanges().Run();
        
      
        
    }
}
