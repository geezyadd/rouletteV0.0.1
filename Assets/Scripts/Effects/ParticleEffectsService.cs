using UnityEngine;
using Zenject;

public class ParticleEffectsService : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;
    private ParticlesConfig _particlesConfig;

    [Inject]
    private void InjectDependencies(ParticlesConfig particlesConfig)
    {
        _particlesConfig = particlesConfig;
    }

    public void SpawnParticle(SlotType type)
    {
        GameObject particleGameObject = null;
        foreach(ParticlesHolder particle in _particlesConfig.Particles)
        {
            if(particle.Type == type)
            {
                particleGameObject = particle.Particle;
            }
        }
        if(particleGameObject != null)
        {
            GameObject particleSystem = Instantiate(particleGameObject, _spawnPoint.transform.position, Quaternion.identity);
            particleSystem.GetComponent<ParticleSystem>().Play();
        }
    }
}
