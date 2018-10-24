using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    public GameObject victoryParticlesPrefab;
    public GameObject stepParticlesPrefab;


    //private ParticleSystem victoryPs;
   // private ParticleSystem stepPs;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        /*if (victoryPs == null)
        {
            
            victoryPs.gameObject.SetActive(false);
        }
        if(stepPs == null)
        {
            stepPs = Instantiate(stepParticlesPrefab).GetComponent<ParticleSystem>();
            stepPs.gameObject.SetActive(false);
        }*/
    }


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
    }

    public void playStepParticles(Vector3 positionToPlay)
    {
        playParticles(stepParticlesPrefab, positionToPlay);
    }

    
}
