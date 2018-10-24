using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public GameObject victoryParticlesPrefab;
    public GameObject victoryParticlesPrefab2;
    public GameObject stepParticlesPrefab;
    public GameObject losingParticlesPrefab;

    private void playParticles(GameObject particlesPrefab, Vector3 pos)
    {
            ParticleSystem particles = Instantiate(particlesPrefab).GetComponent<ParticleSystem>();
            particles.gameObject.SetActive(true);
            particles.transform.position = pos;
            particles.Play();
    }


    public void playVictoryParticles(Vector3 positionToPlay)
    {
        playParticles(victoryParticlesPrefab, positionToPlay);
        playParticles(victoryParticlesPrefab2, positionToPlay + new Vector3(0f,1f));
    }

    public void playStepParticles(Vector3 positionToPlay)
    {
        playParticles(stepParticlesPrefab, positionToPlay);
    }

    public void playLosingParticles(Vector3 positionToPlay)
    {
        playParticles(losingParticlesPrefab, positionToPlay);
    }

    
}
