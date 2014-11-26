﻿using UnityEngine;

public class Grapher3 : MonoBehaviour {
	[Range(10,30)]
	public int resolution = 10;
	private int currentResolution;

	private ParticleSystem.Particle[] points;
	// Use this for initialization

	private void CreatePoints(){
		if(resolution < 10 || resolution > 100){
			Debug.LogWarning("Resolution is out of bounds. Resetting to minimum",this);
			resolution = 10;
		}
		currentResolution = resolution;
		points = new ParticleSystem.Particle[resolution * resolution * resolution];
		float increment = 1f / (resolution);
		int i = 0;
		for (int x=0; x < resolution; x++) {
			for(int z = 0; z < resolution; z++){
				for (int y = 0; y<resolution; y++){
					Vector3 p = new Vector3(x , y, z) * increment;
					points[i].position = p;
					points[i].color = new Color(p.x, p.y, p.z);
					points[i++].size = 0.1f;
				}
			}
		}
	}
	public enum FunctionOption {
		Linear,
		Exponential,
		Parabola,
		Sine,
		Ripple
	}
	private delegate float FunctionDelegate (Vector3 p,float t);
	private static FunctionDelegate[] functionDelegates = {
		Linear,
		Exponential,
		Parabola,
		Sine,
		Ripple
	};

	// Update is called once per frame
	void Update () {
		if (currentResolution != resolution || points == null) {
			CreatePoints();
		}
		FunctionDelegate f = functionDelegates[(int)function];
		float t = Time.timeSinceLevelLoad;
		for (int i = 0; i < points.Length; i++) {
			Color c = points[i].color;
			c.a = f(points[i].position,t);
			points[i].color = c;
		}
		particleSystem.SetParticles (points, points.Length);
	}
	private static float Linear(Vector3 p, float t){
		return p.x;
	}
	private static float Exponential (Vector3 p, float t){
		return p.x * p.x;
	}
	private static float Parabola (Vector3 p, float t){
		p.x += p.x - 1f;
		p.z += p.z - 1f;
		return 1f - p.x * p.x * p.z * p.z;
	}

	private static float Sine(Vector3 p, float t){
		return 	0.50f +
			0.25f * Mathf.Sin(4f * Mathf.PI * p.x + 4f * t) * Mathf.Sin(2f * Mathf.PI * p.z + t) +
				0.10f * Mathf.Cos(3f * Mathf.PI * p.x + 5f * t) * Mathf.Cos(5f * Mathf.PI * p.z + 3f * t) +
				0.15f * Mathf.Sin(Mathf.PI * p.x + 0.6f * t);
	}

	private static float Ripple (Vector3 p, float t){
		p.x -= 0.5f;
		p.z -= 0.5f;
		float squareRadius = p.x * p.x + p.z * p.z;
		return 0.5f + Mathf.Sin (15f * Mathf.PI * squareRadius - 2f * t) / (2f + 100f * squareRadius);
	}

	public FunctionOption function;
}
