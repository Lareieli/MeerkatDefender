using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public AudioClip attackSound;
	public AudioClip hurtSound;
	// changed from float to int for testing
	public int health;
	public GameObject projectile;
	public float projectileSpeed;
	public float summonRate;
	
	private float playerSpeed = 20f; 
	private float padding = .75f;
	private float minplayerheightY;
	private float maxplayerheightY;
	//added for health text
	private LivesKeeper livesKeeper;
	
	// Use this for initialization
	void Start () {
		livesKeeper = GameObject.Find ("Lives").GetComponent<LivesKeeper>();		
		livesKeeper.Lives (health);
		float distanceFromCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1,1, distanceFromCamera));
		Vector3 rightBottom = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceFromCamera));
		minplayerheightY = rightBottom.y + padding;
		maxplayerheightY = rightTop.y - padding;
	}
	void SummonAlly (){
		Vector3 startPosition = transform.position + new Vector3(0,0,0);
		// instantiate the player projectile at the player's transform position as a game object
		// with no added rotation = Quaternion.identity		
		GameObject ally = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
		ally.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileSpeed,0,0);
		AudioSource.PlayClipAtPoint (attackSound, transform.position);
	}
	// Update is called once per frame
	void Update () {		
		//instantiating an ally projectile on keypress space bar, repeating until release
		if (Input.GetKeyDown("space")){
			InvokeRepeating("SummonAlly", 0.000001f, summonRate);
		}
		if (Input.GetKeyUp("space")){
			CancelInvoke("SummonAlly");
		}
		if (Input.GetKey("up")) {
			transform.Translate (Vector3.up * playerSpeed * Time.deltaTime);
		} else if (Input.GetKey("down")){
			transform.Translate (Vector3.down * playerSpeed * Time.deltaTime);
		}
		//this is a local variable to clamp the player inside the game space up and down
		float newY = Mathf.Clamp(transform.position.y, minplayerheightY, maxplayerheightY);
		//now we re-set the transform to the new position
		transform.position = new Vector3(transform.position.x, newY, transform.position.z);
	}
	void OnTriggerEnter2D (Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.GetDamage();
			missile.Hit();
			//added for health text
			livesKeeper.Lives(health);
			AudioSource.PlayClipAtPoint (hurtSound, transform.position);
			if (health <=0){
				Die();
			}
		}
	}
	void Die(){
		LevelManager manage = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		manage.LoadLevel("Lose Screen");
		Destroy (gameObject);
	}

}
