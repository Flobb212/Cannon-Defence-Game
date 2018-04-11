using UnityEngine;
using System.Collections;

public class DestroyParticleSystem : MonoBehaviour 
{
    private ParticleSystem m_particle;

	// Use this for initialization
	void Start ()
    {
        m_particle = GetComponent<ParticleSystem>();
        Destroy(gameObject, m_particle.startLifetime);
	}
}
