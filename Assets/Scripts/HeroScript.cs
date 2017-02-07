using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour {

	private int countCoins = 0;

	public GameObject mainCanvas;
	public GameObject dataLevel;

	private bool isDead = false;

	// Use this for initialization
	void Start () {
	
		countCoins = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void restoreLevel(){

		Application.LoadLevel ("CreatorScene");
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Enemy" && !isDead) {

			isDead = true;

			//Debug.Log ("Kill Hero");
			gameObject.GetComponent<BoxCollider2D> ().enabled = false;

			gameObject.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.None;

			//gameObject.GetComponent<Rigidbody2D> ().angularVelocity = 200f;
			gameObject.GetComponent<Rigidbody2D> ().AddTorque (200);

			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 6);

			Invoke ("restoreLevel", 2);

		} else if (other.gameObject.tag == "Zanahoria") {

			Destroy (other.gameObject);

			countCoins++;

			if(countCoins >= 3){

				//Level Completo
				mainCanvas.SetActive (true);
			}
		} else if (other.gameObject.tag == "KillLevel") {

			Application.LoadLevel ("Level" + dataLevel.GetComponent<LevelDataScript>().currentLevel);
		}
	}
}
