using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//ScoreKeeper attached to Score text object in Canvas, updates the point count text on Score text object 

public class ScoreKeeper : MonoBehaviour {

	public static int score = 0;
	private Text myText;
	
	void Start(){
		myText = GetComponent<Text>();
		Reset();
	}
	
	public void Score(int points){
		score += points;
		myText.text = score.ToString();	
	}
	
	public static void Reset(){
		score = 0;
	}
}
