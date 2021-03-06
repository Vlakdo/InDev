﻿using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public GameObject platform;

	public float moveSpeed;

	public Transform[] points;

	public int pointSelection;

	private Transform currentPoint;

	private float currentDistance;

	// Use this for initialization
	void Start () {
	
		currentPoint = points[pointSelection];

		currentDistance = Vector2.Distance (platform.transform.position, currentPoint.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
		platform.transform.position = Vector2.MoveTowards (platform.transform.position, currentPoint.transform.position, Time.deltaTime * moveSpeed);

		currentDistance -= Mathf.Abs (currentDistance - Vector2.Distance (platform.transform.position, currentPoint.transform.position));

		if (currentDistance <= 0) {
		
			pointSelection++;

			if(pointSelection >= points.Length){

				pointSelection = 0;
			}

			currentPoint = points[pointSelection];

			currentDistance = Vector2.Distance (platform.transform.position, currentPoint.transform.position);
		}
	}
}
