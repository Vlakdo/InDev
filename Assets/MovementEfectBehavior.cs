using UnityEngine;
using System.Collections;

public class MovementEfectBehavior : MonoBehaviour {

	public GameObject objReference;

	public float turnSpeed  = 100.0f;

	public float substracAngle = 90;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 vectorToTarget = objReference.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle-substracAngle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);

		//Debug.Log ("EL ANGULO ES:" + angle);
	}
}
