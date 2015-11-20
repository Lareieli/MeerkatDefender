using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
// start with the music player set as null, then if it is null the else statement kicks in
// and asserts that THIS object created is the instance.  On the other hand, if it isn't null
// because it was instantiated in the scene, then destroy (prevent) any new music players from
// being created
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip winClip;
	public AudioClip loseClip;
	
	private AudioSource music;
	
	
	void Awake (){
		if (instance != null && instance != this) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}				
	}
	// Update is called once per frame
	void OnLevelWasLoaded (int musicLevel) {
		Debug.Log ("MusicPlayer: loaded music level " +musicLevel);
		music.Stop ();
		
		if (musicLevel == 0){
			music.clip = startClip;
		}
		if (musicLevel == 1){
			music.clip = gameClip;
		}
		if (musicLevel == 2){
			music.clip = winClip;
		}
		if (musicLevel == 3){
			music.clip = loseClip;
		}
		music.loop = true;
		music.Play ();
	
	}
}
