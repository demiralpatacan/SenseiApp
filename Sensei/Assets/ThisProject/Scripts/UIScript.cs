using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public Button btnGesture, btnNod, btnClone, btnRasengan, btnChidori, btnRecord, btnCall, btnBye, UIChanger;
	public bool recordable= false;

	public Button [] buttonArray;
	private GameObject kakashi;
	// Use this for initialization
	void Start () {
		kakashi = GameObject.Find ("Kakashi");
		buttonArray = new Button[]{ btnGesture, btnNod, btnClone, btnRasengan, btnChidori, btnCall, btnBye };
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeUI(){
		if (!recordable) {
			ButtonsNOTVisible ();
			ButtonsNOTClickable ();
			btnRecord.interactable = true;
			btnRecord.gameObject.SetActive (true);
		} else {
			ButtonsVisible ();
			ButtonsClickable ();
			btnRecord.interactable = false;
			btnRecord.gameObject.SetActive (false);
		}
		recordable = !recordable;
	}

	public void ButtonsNOTClickable(){
		foreach (Button element in buttonArray)
			element.interactable = false;
	}

	public void ButtonsClickable(){
		foreach (Button element in buttonArray)
			element.interactable = true;
	}

	public void ButtonsNOTVisible(){
		foreach (Button element in buttonArray)
			element.gameObject.SetActive (false);
	}

	public void ButtonsVisible(){
		foreach (Button element in buttonArray)
			element.gameObject.SetActive (true);
	}

	public void ButtonCall(){
		ButtonsNOTClickable ();
		kakashi.GetComponent<AnimatorScript> ().IdleAnswer();
	}

	public void ButtonGesture(){
		ButtonsNOTClickable ();
		kakashi.GetComponent<AnimatorScript> ().IdleToTalk ();
	}

	public void ButtonNod(){
		ButtonsNOTClickable ();
		kakashi.GetComponent<AnimatorScript> ().IdleToHeadNod ();
	}

	public void ButtonClone(){
		ButtonsNOTClickable ();
		kakashi.GetComponent<AnimatorScript> ().IdleToBunshin ();
	}

	public void ButtonRasengan(){
		ButtonsNOTClickable ();
		kakashi.GetComponent<AnimatorScript> ().IdleToRasengan ();
	}

	public void ButtonChidori(){
		ButtonsNOTClickable ();
		kakashi.GetComponent<AnimatorScript> ().IdleToChidori ();
	}

	public void ButtonBye(){
		ButtonsNOTClickable ();
		kakashi.GetComponent<AnimatorScript> ().IdleToBye ();
	}


}
