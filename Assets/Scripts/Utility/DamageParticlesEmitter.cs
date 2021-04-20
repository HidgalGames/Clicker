using System.Collections.Generic;
using UnityEngine;

public class DamageParticlesEmitter : MonoBehaviour
{
    public static DamageParticlesEmitter Instance;

    [SerializeField] private List<ParticleSystem> damageParticles;
    [SerializeField] private List<ParticleSystem> deathParticles;

    private List<ParticleSystem> cashedDamageParticles = new List<ParticleSystem>();
    private List<ParticleSystem> cashedDeathParticles = new List<ParticleSystem>();

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlayDamageParticle(Vector3 touchPosition)
    {
        if(cashedDamageParticles.Count > 0)
        {
            ParticleSystem particles = GetCashedParticles(cashedDamageParticles);

            if (particles)
            {
                PlayCashedParticle(particles, touchPosition);
            }
            else
            {
                CreateNewDamageParticles(touchPosition);
            }

        }
        else
        {
            CreateNewDamageParticles(touchPosition);
        }
    }

    public void PlayDeathParticles(Vector3 position)
    {
        if (cashedDeathParticles.Count > 0)
        {
            ParticleSystem particles = GetCashedParticles(cashedDeathParticles);

            if (particles)
            {
                PlayCashedParticle(particles, position);
            }
            else
            {
                CreateNewDamageParticles(position);
            }
        }
        else
        {
            CreateNewDeathParticles(position);
        }
    }

    private void CreateNewDamageParticles(Vector3 position)
    {
        GameObject newParticles = Instantiate(damageParticles[Random.Range(0, damageParticles.Count - 1)].gameObject, position, Quaternion.identity, this.transform);
        cashedDamageParticles.Add(newParticles.GetComponent<ParticleSystem>());
    }

    private void CreateNewDeathParticles(Vector3 position)
    {
        GameObject newParticles = Instantiate(deathParticles[Random.Range(0, deathParticles.Count - 1)].gameObject, position, Quaternion.identity, this.transform);
        cashedDeathParticles.Add(newParticles.GetComponent<ParticleSystem>());
    }

    private void PlayCashedParticle(ParticleSystem particles, Vector3 position)
    {
        particles.gameObject.transform.position = position;
        particles.gameObject.SetActive(true);
        particles.Play();
    }

    private ParticleSystem GetCashedParticles(List<ParticleSystem> cashedList)
    {
        foreach (ParticleSystem particles in cashedList)
        {
            if (!particles.gameObject.activeSelf)
            {
                return particles;
            }
        }

        return null;
    }
}
