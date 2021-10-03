using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class EndlessScreenSystem : SystemBase
{
    protected override void OnUpdate()
    {       
        
        
        
        Entities.ForEach((ref Translation translation, in Rotation rotation, in EndlessScreen endlessScreen) => {
            if(endlessScreen.endlessScreenActivated)
            {
                //Endless border fx custom for screen 16:9
                if (translation.Value.x < -6.25f-0.2f)
                    translation.Value = new float3(6.25f+ 0.2f, translation.Value.y, translation.Value.z);
                if (translation.Value.x > 6.25f+0.5f)
                    translation.Value = new float3(-6.25f- 0.2f, translation.Value.y, translation.Value.z);

                if (translation.Value.z < -3.25f- 0.2f)
                    translation.Value = new float3(translation.Value.x, translation.Value.y, 3.75f+ 0.2f);
                if (translation.Value.z > 3.75f+ 0.2f)
                    translation.Value = new float3(translation.Value.x, translation.Value.y, -3.25f- 0.2f);
            }
           

        }).Schedule();
        Entities.ForEach((ref Asteroid asteroid, ref EndlessScreen endlessScreen, in DynamicBuffer<TriggerBuffer> triggerBuffers) =>
        {
            if(triggerBuffers.Length>0)
            {
                endlessScreen.endlessScreenActivated = true;
            }
        }).Schedule();
    }
}
