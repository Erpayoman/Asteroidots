using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


public class AsteroidSystem : SystemBase
{
    private float spawnerTime;
    private Random random = new Random(56);
    private int maxAsteroidsInGroups = 5;
    private int asteroidsCountdown = 5;   
        
    
    
    protected override void OnUpdate()
    {
        AsteroidSpawner();
        WatchMeExplode();

        

    }

    private void WatchMeExplode()
    {
        Entities.ForEach((ref Asteroid asteroid, ref Kill kill, ref Translation translation) =>
        {
            Entity explosion = EntityManager.Instantiate(asteroid.explosionPrefab);
            EntityManager.SetComponentData(explosion, new Translation { Value = translation.Value });

            int numberAsteroid = 2;

            switch(asteroid.hitStatus)
            {
                case 3:
                    while(numberAsteroid > 0)
                    {
                        Entity asteroidPrefab2 = EntityManager.Instantiate(asteroid.asteroidPrefab2);
                        EntityManager.SetComponentData(asteroidPrefab2, new Translation { Value = translation.Value });
                        //EntityManager.SetComponentData(asteroidPrefab2, new CompositeScale { Value = scale.Value / 2 });
                        EntityManager.SetComponentData(asteroidPrefab2, new Movable
                        {
                            speed = random.NextFloat(2f, 3f),
                            direction = math.normalize(random.NextFloat3())
                        });

                        numberAsteroid--;
                    }
                   

                break;

                
                case 2:
                    while (numberAsteroid > 0)
                    {
                        Entity asteroidPrefab1 = EntityManager.Instantiate(asteroid.asteroidPrefab1);
                        EntityManager.SetComponentData(asteroidPrefab1, new Translation { Value = translation.Value });
                        //EntityManager.SetComponentData(asteroidPrefab1, new CompositeScale { Value = scale.Value / 2 });
                        EntityManager.SetComponentData(asteroidPrefab1, new Movable
                        {
                            speed = random.NextFloat(2f, 4f),
                            direction = math.normalize(random.NextFloat3())
                        });
                        numberAsteroid--;
                    }
                break;
            }

        }).WithStructuralChanges().WithoutBurst().Run();
    }

    private void AsteroidSpawner()
    {
        spawnerTime -= Time.DeltaTime;

        if (spawnerTime <= 0f)
        {
            spawnerTime = 1.5f;

            Entities.ForEach((ref AsteroidManager asteroidManager) =>
            {

                Entity spawnedAsteroid = EntityManager.Instantiate(asteroidManager.asteroidPrefab3);
                

                asteroidsCountdown--;

                if (asteroidsCountdown == 0)
                {
                    spawnerTime = 15f;
                    asteroidsCountdown = maxAsteroidsInGroups;
                }

                EntityManager.SetComponentData(spawnedAsteroid, new Translation
                {
                    Value = new float3((random.NextBool()) ? random.NextFloat(6f, 9f) : random.NextFloat(-6f, -9f),
                                                                                                                         1,
                                                                                                      (random.NextBool()) ? random.NextFloat(4f, 7f) : random.NextFloat(-4f, -7f))
                });
                EntityManager.SetComponentData(spawnedAsteroid, new Movable
                {
                    direction = new float3(random.NextFloat(-5f, 5f), 1, random.NextFloat(-3f, 3f)) - EntityManager.GetComponentData<Translation>(spawnedAsteroid).Value,
                    speed = random.NextFloat(0.1f, 0.3f)

                });
            }).WithStructuralChanges().WithoutBurst().Run();
        }
    }
}
