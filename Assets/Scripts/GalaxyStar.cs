using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ParticleSystem))]
public class GalaxyStar : MonoBehaviour
{
	public float starSize = 0.1f;

	public float startSizeRange = 0.5f;

	public Color color = Color.white;

	private Vector3[] allPos;

	private ParticleSystem particles;

	private ParticleSystem.Particle[] stars;

	public void Init(Vector3[] posList)
	{
		if (posList != null && posList.Length > 0)
		{
			allPos = posList;
			stars = new ParticleSystem.Particle[posList.Length];
			particles = GetComponent<ParticleSystem>();
			var main = particles.main;
			main.simulationSpeed = 0;
			for (int i = 0; i < posList.Length; i++)
			{
				float num = Random.Range(startSizeRange * 0.5f, startSizeRange * 1.5f);
				stars[i].position = posList[i];
				stars[i].startSize = starSize * num;
				stars[i].startColor = color;
			}
			particles.SetParticles(stars, stars.Length);
		}
	}

	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			Init(allPos);
		}
	}
	
}
