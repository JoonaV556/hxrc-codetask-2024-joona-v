using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject DeathParticle;
    public GameObject ScoreParticle;

    private void OnEnable()
    {
        DeathHandler.OnPlayerDied += SpawnDeathParticle;
        ScoreConsumable.OnScoreConsumableConsumed += SpawnScoreParticle;
    }

    private void OnDisable()
    {
        DeathHandler.OnPlayerDied -= SpawnDeathParticle;
        ScoreConsumable.OnScoreConsumableConsumed -= SpawnScoreParticle;
    }

    void SpawnDeathParticle(DeathHandler.DeathInfo deathInfo)
    {
        GameObject deathParticle = Instantiate(DeathParticle, deathInfo.position, Quaternion.identity);
        deathParticle.AddComponent<ParticleKiller>(); // Kill particle objects after they are played
    }

    void SpawnScoreParticle(ConsumeInfo info)
    {
        GameObject scoreParticle = Instantiate(ScoreParticle, info.position, Quaternion.identity);
        scoreParticle.AddComponent<ParticleKiller>(); // Kill particle objects after they are played
    }
}
