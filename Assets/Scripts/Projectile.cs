using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	// changed from float to int for testing
 	public int damage;
 	
	// changed from float to int for testing
 	public int GetDamage(){
 		return damage;
 	}
 	public void Hit(){
 		Destroy(gameObject);
 	}
 	
}
