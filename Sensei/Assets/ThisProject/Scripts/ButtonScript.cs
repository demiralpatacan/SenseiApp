using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Grpc.Auth;*/

public class ButtonScript : MonoBehaviour {

	private bool isRecording;
	// Use this for initialization
	void Start () {
		isRecording = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClicked(){
		isRecording = !isRecording;
		if (isRecording)
			RecordSpeech ();
		else
			StopRecording ();
	}

	public void RecordSpeech(){
		Debug.Log ("Recording...");
	}

	public void StopRecording(){
		Debug.Log ("Recording Stop");
	}
}
