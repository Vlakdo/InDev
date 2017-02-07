using UnityEngine;
using System.Collections;

public class GroundHeroScript : MonoBehaviour {

	public GameObject hero;

	private bool disableConstrain = false;

	private GameObject Platf;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		float tempSpeed = hero.GetComponent<Rigidbody2D> ().velocity.x + hero.GetComponent<Rigidbody2D> ().velocity.y;

		if (tempSpeed == 0 && disableConstrain) {

			disableConstrain = false;

			//hero.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;

			Platf.GetComponent<TrianglePlatfScript> ().SetTrianglePhysicsMaterials ();
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.tag == "Triangle") {

			//other.GetComponent<TrianglePlatfScript> ().SetTrianglePhysicsMaterials ();

			Platf = other.gameObject;

			//hero.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;
			//Debug.Log(other.gameObject.tag);
			disableConstrain = true;

		} else if(other.gameObject.tag == "MovingPlatform"){

			hero.transform.parent = other.transform;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		
		if (other.gameObject.tag == "Triangle") {

			other.GetComponent<TrianglePlatfScript> ().SetNormalPhysicsMaterials ();

			//hero.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
			//hero.transform.eulerAngles = new Vector3 (0, 0, 0);
		} else if(other.gameObject.tag == "MovingPlatform"){

			hero.transform.parent = null;
		}
	}
}
