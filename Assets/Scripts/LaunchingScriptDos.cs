using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchingScriptDos : MonoBehaviour {

	public GameObject Hero;

	public List<GameObject> trajectoryPointList = new List<GameObject>();

	public float speed = 2f;

	private bool simulateTrajectory = true;


	//Variables para la validacion de que el heroe esta en el suelo
	private bool isGrounding = true;
	public Transform checkGround;
	private float checkRadio = 0.2f;
	public LayerMask groundMask;

	public Transform initImpulsePoint;
	private Vector2 startTouch;
	private Vector2 startImpulsePoint = new Vector2 (0f, 4.4f);

	//Zoom Camera:
	public Transform pointMoveCamera;
	private Vector2 destPoint;
	private float currentDistance;
	private bool valideDistance = false;
	public GameObject cameraM;

	// Use this for initialization
	void Start () {
		//Time.timeScale = 0.2f;
	}

	void FixedUpdate() {

		if (valideDistance) {

			currentDistance -= Mathf.Abs (currentDistance - Vector2.Distance (Hero.transform.position, destPoint));

			if(currentDistance <= 0){

				valideDistance = false;
				cameraM.GetComponent<ZoomCameraScript> ().PauseSlowMotion ();
				cameraM.transform.parent = Hero.transform;
				Time.timeScale = 0.3f;
			}
		}

		/*
		if (!simulateTrajectory) {
			GameObject gun = Hero.transform.Find ("Sprite").gameObject;
			//gun.transform.Rotate(Vector2.right * Time.deltaTime);
			gun.transform.Rotate (Vector3.forward * -5);
		}*/
	}
	
	// Update is called once per frame
	void Update () {

		ValideHeroInGround ();

		Vector2 v2 = Input.mousePosition;

		v2 = Camera.main.ScreenToWorldPoint(v2);

		if (Input.GetMouseButtonDown (0) && simulateTrajectory) {

			startTouch = Input.mousePosition;

			startTouch = Camera.main.ScreenToWorldPoint(startTouch);
		}

		if(Input.GetMouseButton (0) && simulateTrajectory){

			float difX = v2.x - startTouch.x;
			float difY = v2.y - startTouch.y;

			initImpulsePoint.localPosition = new Vector2(startImpulsePoint.x + (difX*2), startImpulsePoint.y + (difY*2));

			ShowTrajectoryPoints(initImpulsePoint.position);
		}

		if(Input.GetMouseButtonUp(0) && simulateTrajectory){
			//HABILITAR SLOW ++++++++
			/*
			destPoint = pointMoveCamera.position;
			currentDistance = Vector2.Distance (Hero.transform.position, destPoint);
			valideDistance = true;
			*/

			//Continue
			simulateTrajectory = false;

			//Vector2 dirBetweenVectors = new Vector2(v2.x+difTouchX, v2.y+difTouchY) - new Vector2(Hero.transform.position.x, Hero.transform.position.y);
			Vector2 dirBetweenVectors = new Vector2(initImpulsePoint.position.x, initImpulsePoint.position.y) - new Vector2(Hero.transform.position.x, Hero.transform.position.y);
			//dirBetweenVectors.x *= -1;

			//Hero.GetComponent<Rigidbody2D>().velocity = new Vector2(dirBetweenVectors.x, dirBetweenVectors.y) * speed;
			Hero.GetComponent<Rigidbody2D>().velocity = new Vector2(dirBetweenVectors.x, dirBetweenVectors.y);

			//HABILITAR SLOW ++++++++
			//cameraM.GetComponent<ZoomCameraScript> ().SetSlowMotion ("In", pointMoveCamera.position.x, pointMoveCamera.position.y);

			ClearTrayectoryPoints ();

			//cameraM.GetComponent<ZoomCameraScript> ().SetSlowMotion ("In");
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

		float tempSpeed = 0;//Hero.GetComponent<Rigidbody2D> ().velocity.x + Hero.GetComponent<Rigidbody2D> ().velocity.y;

		//Debug.Log (tempSpeed);

		if (isGrounding && !simulateTrajectory && tempSpeed == 0) {

			simulateTrajectory = true;

			cameraM.transform.parent = null;

			cameraM.GetComponent<ZoomCameraScript> ().IntermediateSlowMotion ();
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
		Vector2 v2 = new Vector2(pos.x, pos.y) - new Vector2(Hero.transform.position.x, Hero.transform.position.y);

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
