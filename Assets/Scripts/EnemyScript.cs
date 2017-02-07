using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	private Rigidbody2D rigiBody2d;

	public float minRangueRotationSpeed = 10f;
	public float maxRangueRotationSpeed = 10f;
	public float maxRangueMovementSpeed = 10f;

	// Use this for initialization
	void Awake () {

		rigiBody2d = GetComponent<Rigidbody2D> ();

		//rigiBody2d.AddTorque(Random.Range(minRangueRotationSpeed,maxRangueRotationSpeed) * (int)Choose(1, -1));

		//ShootIntheDirectionOfTheHero();

	}

	void OnEnable()
	{
		float newAsteroidScale = Random.Range (0.5f, 1.5f);

		gameObject.transform.localScale = new Vector2 (newAsteroidScale, newAsteroidScale);

		rigiBody2d.AddTorque(Random.Range(minRangueRotationSpeed,maxRangueRotationSpeed) * (int)Choose(1, -1));
		
		ShootIntheDirectionOfTheHero();
	}

	object Choose(object a, object b, params object[] p){
		
		int random = Random.Range(0, p.Length + 2);
		if (random == 0) return a;
		if (random == 1) return b;
		return p[random - 2];
		
	}

	void ShootIntheDirectionOfTheHero(){

		GameObject hero = GameObject.FindGameObjectWithTag("hero");
		
		float speedX = (hero.transform.position.x - transform.position.x) * maxRangueMovementSpeed;
		float speedY = (hero.transform.position.y - transform.position.y) * maxRangueMovementSpeed;
		
		rigiBody2d.AddForce(new Vector2(speedX, speedY));
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "destroyer"){

			//gameObject.Recycle();
		}
	}
}
