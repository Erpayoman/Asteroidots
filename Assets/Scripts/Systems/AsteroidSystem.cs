using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class AsteroidSystem : ComponentSystem
{
    private float spawnerTime;
    private Random random;
    private int maxAsteroidsInGroups;
    private int asteroidsCountdown;
    
    protected override void OnCreate()
    {
        random = new Random(56);
        maxAsteroidsInGroups = 5;
        asteroidsCountdown = 5;
        
    }
    protected override void OnUpdate()
    {
        spawnerTime -= Time.DeltaTime;

        if(spawnerTime <= 0f)
        {
            spawnerTime = 1.5f;

            Entities.ForEach((ref PrefabEntityAsteroid prefabEntityAsteroid)=>{

                Entity spawnedAsteroid = EntityManager.Instantiate(prefabEntityAsteroid.asteroidPrefab);
                asteroidsCountdown--;
                
                if(asteroidsCountdown == 0)
                {
                    spawnerTime = 15f;
                    asteroidsCountdown = 5;
                }

                EntityManager.SetComponentData(spawnedAsteroid, new Translation { Value = new float3((random.NextBool())?random.NextFloat(6f, 9f): random.NextFloat(-6f, -9f),
                                                                                                                         1, 
                                                                                                      (random.NextBool())?random.NextFloat(4f, 7f): random.NextFloat(-4f, -7f)) });
                EntityManager.SetComponentData(spawnedAsteroid, new Movable
                {
                    direction = new float3(random.NextFloat(-5f, 5f), 1, random.NextFloat(-3f, 3f)) - EntityManager.GetComponentData<Translation>(spawnedAsteroid).Value,
                    speed = 0.1f

                }) ;
            }) ;
        }
        
    }
}
