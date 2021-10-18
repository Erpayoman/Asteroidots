using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class DamageSystem : SystemBase
{
    float explosion1Volumen = 0.01f;
    float explosion2Volumen = 0.01f;
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
        
        
        Entities.ForEach((DynamicBuffer<CollisionBuffer> colBuffer, ref Health health) => {
           
            for(int i = 0; i < colBuffer.Length; i++)
            {
                if(health.invicibleTimer <=0 && HasComponent<Damage>(colBuffer[i].entity))
                {
                    health.value -= GetComponent<Damage>(colBuffer[i].entity).value;
                    health.invicibleTimer = 1;
                }
            }
        }).Schedule();

        Entities
            .WithNone<Kill>()
            .ForEach((Entity ent, ref Health health) =>
        {
            health.invicibleTimer -= dt;
            if (health.value <= 0)
                EntityManager.AddComponentData(ent, new Kill() { timer = health.killTimer });
                
        }).WithStructuralChanges().Run();

        var ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        var ecb = ecbSystem.CreateCommandBuffer();

        Entities.
            ForEach((Entity e, ref Kill kill) =>
        {
            kill.timer -= dt;
            if (kill.timer <= 0)
            {
                if(EntityManager.HasComponent<Asteroid>(e) && EntityManager.HasComponent<Scoring>(e))
                {
                    AudioManager.instance.PlayFX("explosion2", explosion2Volumen);

                    ScoreManager.instance.AddScore(EntityManager.GetComponentData<Scoring>(e).scoreValue);
                }
                if (EntityManager.HasComponent<Player>(e))
                {
                    
                    AudioManager.instance.PlayFX("explosion1", explosion1Volumen);
                }


                ecb.DestroyEntity(e);
                
            }               



        }).WithoutBurst().Run();
        ecbSystem.AddJobHandleForProducer(this.Dependency);
    }
}
