using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchingScript : MonoBehaviour {

	public GameObject Hero;

	public List<GameObject> trajectoryPointList = new List<GameObject>();

	public float speed = 2f;

	private bool simulateTrajectory = true;

	private int difTouchX = 0;
	private int difTouchY = 8;
	//private int difTouchY = 8;

	//Variables para la validacion de que el heroe esta en el suelo
	private bool isGrounding = true;
	public Transform checkGround;
	private float checkRadio = 0.2f;
	public LayerMask groundMask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		ValideHeroInGround ();

		Vector2 v2 = Input.mousePosition;

		v2 = Camera.main.ScreenToWorldPoint(v2);
		/*
		if(Hero.transform.position.x < v2.x){
			difTouchX = -6;
		} else if(Hero.transform.position.x > v2.x){
			difTouchX = 6;
		}
		*/
		if(Input.GetMouseButton (0) && simulateTrajectory){

			ShowTrajectoryPoints(v2);
		}

		if(Input.GetMouseButtonUp(0) && simulateTrajectory){

			simulateTrajectory = false;

			Vector2 dirBetweenVectors = new Vector2(v2.x+difTouchX, v2.y+difTouchY) - new Vector2(Hero.transform.position.x, Hero.transform.position.y);
			//dirBetweenVectors.x *= -1;

			//Hero.GetComponent<Rigidbody2D>().velocity = new Vector2(dirBetweenVectors.x, dirBetweenVectors.y) * speed;
			Hero.GetComponent<Rigidbody2D>().velocity = new Vector2(dirBetweenVectors.x, dirBetweenVectors.y);

			ClearTrayectoryPoints ();
		}

		//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
		/*
		if (Input.touchCount > 0) {
			var touch = Input.GetTouch(0);

			// Handle finger movements based on touch phase.
			switch (touch.phase) {
			// Record initial touch position.
			case TouchPhase.Began:

				if(simulateTrajectory){

					ShowTrajectoryPoints(v2);
				}

				break;

				// Determine direction by comparing the current touch position with the initial one.
			case TouchPhase.Moved:

				if(simulateTrajectory){

					ShowTrajectoryPoints(v2);
				}

				break;

			case TouchPhase.Stationary:

				if(simulateTrajectory){

					ShowTrajectoryPoints(v2);
				}

				break;

				// Report that a direction has been chosen when the finger is lifted.
			case TouchPhase.Ended:

				if(simulateTrajectory){

					simulateTrajectory = false;

					Vector2 dirBetweenVectors = new Vector2(v2.x+difTouchX, v2.y+difTouchY) - new Vector2(Hero.transform.position.x, Hero.transform.position.y);

					dirBetweenVectors.x *= -1;

					//Hero.GetComponent<Rigidbody2D>().velocity = new Vector2(dirBetweenVectors.x, dirBetweenVectors.y) * speed;
					Hero.GetComponent<Rigidbody2D>().velocity = new Vector2(dirBetweenVectors.x, dirBetweenVectors.y);

					ClearTrayectoryPoints ();
				}

				break;
			}
		}
		*/
	}

	void ValideHeroInGround () {

		isGrounding = Physics2D.OverlapCircle (checkGround.position, checkRadio, groundMask);

		float tempSpeed = Hero.GetComponent<Rigidbody2D> ().velocity.x + Hero.GetComponent<Rigidbody2D> ().velocity.y;

		//Debug.Log (tempSpeed);

		if (isGrounding && !simulateTrajectory && tempSpeed == 0) {

			simulateTrajectory = true;
		}

		if (!isGrounding && simulateTrajectory) {
		
			simulateTrajectory = false;

			ClearTrayectoryPoints ();
		}
	}

	void ClearTrayectoryPoints (){

		foreach(GameObject objPoint in trajectoryPointList){

			objPoint.transform.position = new Vector2 (-10f, 0f);
		}
	}
		
	void ShowTrajectoryPoints(Vector3 pos)
	{
		Vector2 v2 = new Vector2(pos.x+difTouchX, pos.y+difTouchY) - new Vector2(Hero.transform.position.x, Hero.transform.position.y);

		//v2 = new Vector2 (v2.x, v2.y)*speed;
		//v2.x *= -1;

		//Debug.Log (v2.x);

		int segmentCount = 8;
		//float segmentScale = 2;
		Vector2[] segments = new Vector2[segmentCount];

		// The first line point is wherever the player's cannon, etc is
		segments[0] = Hero.transform.position;

		// The initial velocity
		//Vector2 segVelocity = new Vector2(v2.x, v2.y) * speed;
		Vector2 segVelocity = new Vector2(v2.x, v2.y);

		for (int i = 1; i < segmentCount; i++)
		{
			float time2 = i * Time.fixedDeltaTime * 5;
			segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
		}
			
		for (int i = 0; i < segmentCount; i++) {
			
			trajectoryPointList [i].transform.position = new Vector2 (segments [i].x, segments [i].y);
		}
	}
}
