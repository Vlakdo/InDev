using UnityEngine;
using System.Collections;

public class ZoomCameraScript : MonoBehaviour {

	public float zoomSize = 5;
	public Transform hero;
	public float moveSpeed = 2.0f;
	public float substracSizeCam = 0.02f;

	private float currentDistance;

	private string slowMotionState = "none";

	private float destX;
	private float destY;

	// Use this for initialization
	void Start () {
	
		currentDistance = Vector2.Distance (transform.position, new Vector3 (hero.position.x, hero.position.y, transform.position.z));
	}

	// Update is called once per frame
	void Update () {

		if (slowMotionState == "In") {
			
			InSlowMotion ();

		} else if (slowMotionState == "Wait") {

			//transform.position = new Vector3 (hero.position.x, hero.position.y, transform.position.z);
			transform.position = new Vector3 (destX, destY, transform.position.z);

		} if (slowMotionState == "Out") {
		
			OutSlowMotion ();
		}

		/*
		if (Time.timeScale > 0.5f){

			Time.timeScale -= 0.005f;
		}
		*/
	}

	public void SetSlowMotion(string state, float x, float y){
	
		destX = x;
		destY = y;

		currentDistance = Vector2.Distance (transform.position, new Vector3 (destX, destY, transform.position.z));

		slowMotionState = state;
	}

	void InSlowMotion(){

		if (currentDistance > 0) {

			if (zoomSize > 2) {
			
				zoomSize -= substracSizeCam;
			} else {
			
				zoomSize = 2;
			}

			GetComponent<Camera> ().orthographicSize = zoomSize;

			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (destX, destY, transform.position.z), Time.deltaTime * moveSpeed);

			currentDistance -= Mathf.Abs (currentDistance - Vector3.Distance (transform.position, new Vector3 (destX, destY, transform.position.z)));

		} /*else {

			Time.timeScale = 0.4f;

			//transform.position = new Vector3 (hero.position.x, hero.position.y, transform.position.z);
			transform.position = new Vector3 (destX, destY, transform.position.z);

			Debug.Log (Time.timeScale);

			slowMotionState = "Wait";

			Invoke ("IntermediateSlowMotion", 1.0f);
		}*/
	}

	public void IntermediateSlowMotion(){

		Time.timeScale = 1.0f;
	
		currentDistance = Vector2.Distance (transform.position, new Vector3 (0, 0, transform.position.z));

		slowMotionState = "Out";
	}

	void OutSlowMotion(){

		if (currentDistance > 0 || zoomSize < 5) {

				zoomSize += substracSizeCam;

				GetComponent<Camera> ().orthographicSize = zoomSize;

				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (0, 0, transform.position.z), Time.deltaTime * moveSpeed);

				currentDistance -= Mathf.Abs (currentDistance - Vector3.Distance (transform.position, new Vector3 (0, 0, transform.position.z)));

			} else {

				GetComponent<Camera> ().orthographicSize = 5;

				transform.position = new Vector3 (0, 0, transform.position.z);

				Debug.Log (Time.timeScale);

				slowMotionState = "none";
			}
		
	}

	public void PauseSlowMotion(){

		slowMotionState = "Pause";
	}
}
