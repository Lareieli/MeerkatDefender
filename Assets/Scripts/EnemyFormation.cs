using UnityEngine;
using System.Collections;

//EnemyFormation attached to Enemy Formation empty game object, controls the spawning of enemies

public class EnemyFormation : MonoBehaviour {

	public GameObject rufus;
	public GameObject cape;
	public GameObject marshal;
	
	public float gizmoWidth;
	public float gizmoHeight;
	public float enemySpeed; 
	public float spawnDelay;	

	// set the enemy spawner to be moving up initially
	private bool movingUp = true;
	private float minenemyheightY;
	private float maxenemyheightY;

	// Use this for initialization
	void Start () {
	//distance to camera doesn't really matter, writing it out just shows the calculation that is being made
	//to find the camera's z axis location, to include in the Vector3 "address" (x,y,z coordinates)
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		//defines the top and bottom boundaries of the screen for later use using 0,1 as the upper left and
		//0,0 as the bottom left.  Left doesn't matter, but all three coordinates must be included when using
		// Vector3
		Vector3 topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,1, distanceToCamera));
		Vector3 bottomBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera));
		// now we can use the boundaries to constrain the enemy height, so we assign the enemy max and min to 
		//the y component of the vector3 boundaries we just defined
		minenemyheightY = bottomBoundary.y;
		maxenemyheightY = topBoundary.y;
		// a function to be called at the beginning because it is in the start function, creating a new enemy 
		// as a child and at the location of each enemy position object, which are children of the enemy formation 
		// object
		SpawnUntilFull();
		Debug.Log(transform.GetChild(0).GetChild(0));
		Debug.Log(transform.childCount);
		}	
	void SpawnUntilFull(){
		Transform openPosition = NextOpenPosition();
			//if this thing in parenthesis exists, then instantiate an enemy prefab in the open position
			if (openPosition){
			GameObject enemy = Instantiate(rufus, openPosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = openPosition;
			}
			if (NextOpenPosition()){
				// invoke method takes a method name to "activate" and an increment of time for when to activate the method
				Invoke ("SpawnUntilFull", spawnDelay);
			}
		}
	// draws a box in the editor around the enemy formation
	public void OnDrawGizmos (){
		Gizmos.DrawWireCube(transform.position, new Vector3(gizmoWidth, gizmoHeight));
	}
	// Update is called once per frame
	void Update () {	
		// this controls the movement of the formation gizmo
		// since movingUp is set to true above, sets the enemy formation initially moving up
		if (movingUp) {
			transform.Translate (Vector3.up * enemySpeed * Time.deltaTime);
		} 
		// otherwise (later, when we set moving up to not moving up (with !movingUp)) the enemy formation
		// is moving down
		else {
			transform.Translate (Vector3.down * enemySpeed * Time.deltaTime);
			// vector3.up, vector3.down, vector3.right, vector3.left are normalized vectors pointing in that 
			//direction the length of a normalized vector is 1
		}
		// setting the top of the formation to the middle of the gizmo plus half the previously set gizmo height, 
		// and the bottom of the formation to the middle of the gizmo minus half the height
		float formationTop = transform.position.y - (0.5f * gizmoHeight);
		float formationBottom = transform.position.y + (0.5f * gizmoHeight);
		// saying that IF the top of the formation gizmo hits the top of the  top boundary, OR vice versa,
		// if the bottom of the formation gizmo hits the bottom boundary of the screen, then either way,
		// switch the direction
		if (formationTop < minenemyheightY){
			 movingUp = true;
		} else if (formationBottom > maxenemyheightY){
			movingUp = false;
		}
		// check if enemies have been destroyed
		if(AllMembersDead()){
			SpawnUntilFull();
		}
	}
	Transform NextOpenPosition(){
		//loop through and count each child of the position game object (big T Transform is the location/rotation/scale component, little t transform is the object's location/rotation/scale relative to the object's parent)
		foreach(Transform childOfPositionGameObject in transform){
			// if the count of all children of position game object equals zero, return the next open position 
			if (childOfPositionGameObject.childCount == 0){
				return childOfPositionGameObject;
			}
		}
		return null;
	}
	bool AllMembersDead(){
		foreach(Transform childOfPositionGameObject in transform){
			//so long as any child of position game objects have a member, all members are not dead and this method
			// returns false. if all child positions ARE empty, then all members are dead.  If all members are dead, 
			// the AllMembersDead method in the Update method instantly spawns a new enemy in every old position.
			if (childOfPositionGameObject.childCount > 0){
				return false;
			}
		}
		return true;
	}
}
