using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//LivesKeeper attached to Lives text object in Canvas, updates the life count text on Life text object 

public class LivesKeeper : MonoBehaviour {

	//private int lives = 3;
	private Text myText;
	
	void Awake()
	{
		myText = GetComponent<Text>();		
	}
	
	public void Lives(int life){
		myText.text = life <= 0 ? "0" : life.ToString();		
//		if (life <=0){
//			myText.text = "0";
//		} else {			
//			myText.text = life.ToString();	
//		}
	}
}
