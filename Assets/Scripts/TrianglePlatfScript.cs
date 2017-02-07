using UnityEngine;
using System.Collections;

public class TrianglePlatfScript : MonoBehaviour {

	public float rotationsPerMinute = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate(0,0,6.0f*rotationsPerMinute*Time.deltaTime);
	}

	public void SetTrianglePhysicsMaterials(){
		
		gameObject.GetComponent<Collider2D> ().sharedMaterial.friction = 0.1f;

		Debug.Log (gameObject.GetComponent<Collider2D> ().sharedMaterial.friction);
	}

	public void SetNormalPhysicsMaterials(){
		
		gameObject.GetComponent<Collider2D> ().sharedMaterial.friction = 1.0f;

		Debug.Log (gameObject.GetComponent<Collider2D> ().sharedMaterial.friction);
	}
}
