using UnityEngine;
using System.Collections;

public class MoveSpriteBehavior : MonoBehaviour {

	float speed  = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 move = new Vector3 (transform.position.x + 0.02f, transform.position.y, transform.position.z);
	
		transform.position = new Vector3 (transform.position.x + 0.02f, transform.position.y, transform.position.z);//0.01f * speed * Time.deltaTime;
	}
}
