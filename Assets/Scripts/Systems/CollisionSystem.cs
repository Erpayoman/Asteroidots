using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class CollisionSystem : SystemBase
{
    private struct CollisionSystemJob : ICollisionEventsJob
    {
        //public ComponentDataFromEntity<Player> players;
        public BufferFromEntity<CollisionBuffer> collisions;
        public void Execute(CollisionEvent collisionEvent)
        {
            try
            {
                if (collisions.HasComponent(collisionEvent.EntityA))
                {
                    collisions[collisionEvent.EntityA].Add(new CollisionBuffer() { entity = collisionEvent.EntityB });
                }
                if (collisions.HasComponent(collisionEvent.EntityB))
                {
                    collisions[collisionEvent.EntityA].Add(new CollisionBuffer() { entity = collisionEvent.EntityA });
                }
            }
            catch(Exception e)
            {
                Debug.Log("One of the entities was destroyed when it's called in this method");
            }
            
        }
    }
    private struct TriggerSystemJob : ITriggerEventsJob
    {
        //public ComponentDataFromEntity<Player> players;
        public BufferFromEntity<TriggerBuffer> triggers;
        public void Execute(TriggerEvent triggerEvent)
        {
            try
            {
                if (triggers.HasComponent(triggerEvent.EntityA))
                {
                    triggers[triggerEvent.EntityA].Add(new TriggerBuffer() { entity = triggerEvent.EntityB });
                }
                if (triggers.HasComponent(triggerEvent.EntityB))
                {
                    triggers[triggerEvent.EntityA].Add(new TriggerBuffer() { entity = triggerEvent.EntityA });
                }
            }
            catch(Exception e)
            {
                Debug.Log("One of the entities was destroyed when it's called in this method");
            }
            
        }
    }
    
    protected override void OnUpdate()
    {
        var pw = World.GetOrCreateSystem<BuildPhysicsWorld>().PhysicsWorld;
        var sim = World.GetOrCreateSystem<StepPhysicsWorld>().Simulation;

        Entities.ForEach( (DynamicBuffer<CollisionBuffer> collisions) =>
        {
            collisions.Clear();

        }).Run();
        Entities.ForEach((DynamicBuffer<TriggerBuffer> triggers) =>
        {
            triggers.Clear();

        }).Run();


        var colJobHandler = new CollisionSystemJob()
        {
            collisions = GetBufferFromEntity<CollisionBuffer>()
        
        }.Schedule(sim, ref pw, this.Dependency);

        
        var trigJobHandler = new TriggerSystemJob()
        {
            triggers = GetBufferFromEntity<TriggerBuffer>()

        }.Schedule(sim, ref pw, this.Dependency);

        colJobHandler.Complete();
        trigJobHandler.Complete();
    }
}
