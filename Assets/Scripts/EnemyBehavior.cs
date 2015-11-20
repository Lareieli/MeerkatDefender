using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public AudioClip attackSound;
	public AudioClip deathSound;
	public GameObject projectile;
	public float projectileSpeed;
	// changed from float to int for testing
	public int health;
	public float attacksPerSecond;
	public int deathValue;
	
	private ScoreKeeper scoreKeeper;
	
	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	}
	
	void Update(){
		float probability = Time.deltaTime * attacksPerSecond;
		//random value takes the attacks per second and makes them approximately 80% likely to happen
		if (Random.value < probability){
			Attack();
		}
	}
		
	void Attack(){
		// give the enemy projectile a start position away from the firing object so that collision doesn't kill it 
		//instantly
		Vector3 startPosition = transform.position + new Vector3(0,0,0);
		// Instantiate (enemy projectile, startPosition, Quaternion.identity);
		// myProjectile is the chosen enemy projectile, so spit, echo, or swipe depending on the enemy
		GameObject myProjectile = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		myProjectile.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed,0,0);
		AudioSource.PlayClipAtPoint (attackSound, transform.position);
	}
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			if (health <=0){
				Destroy (gameObject);
				scoreKeeper.Score(deathValue);
				AudioSource.PlayClipAtPoint (deathSound, transform.position);
			}
		}
	}
}
