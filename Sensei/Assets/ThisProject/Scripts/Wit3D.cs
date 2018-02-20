/***********************************************************************************
MIT License

Copyright (c) 2016 Aaron Faucher

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in all
	copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
	SOFTWARE.

***********************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
// using System.Web;

public class Wit3D : MonoBehaviour {

	// Audio variables
	public AudioClip commandClip;
	int samplerate;
	bool btnRecClicked= false;

	// API access parameters
	string url;
	string token;
	UnityWebRequest wr;

	// Movement variables
	public float moveTime;
	public float yOffset;
	private bool relocated = false;
	private Vector3 relocationPos;

	// GameObject to use as a default spawn point
	public GameObject kakashi;
	public Button btnRec;
	public GameObject btnRecChild;
	private Image m_Image;
	public Sprite recSprite, rec2Sprite;
	//public Text DebugText;

	// Use this for initialization
	void Start () {

		// If you are a Windows user and receiving a Tlserror
		// See: https://github.com/afauch/wit3d/issues/2
		// Uncomment the line below to bypass SSL
		// System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
		gameObject.transform.position = new Vector3(-100000f, -100000f, -100000f);
		// set samplerate to 16000 for wit.ai
		samplerate = 16000;
		btnRec.interactable = false;
		m_Image = btnRec.GetComponent<Image>();
		m_Image.sprite = rec2Sprite;
		//DebugText.text = "Name: " + Microphone.devices;
	}

	void Update () {

	}

	public void ReLocate(){
		relocationPos = gameObject.transform.position;
		relocated = true;
	}

	public void ChangeTexture(){
		btnRec.interactable = true;
		m_Image.sprite = recSprite;
	}
		
	public void StartRecording(){
		btnRecClicked = !btnRecClicked;
		if (btnRecClicked) {
			print ("started");
			m_Image.sprite = rec2Sprite;
			btnRecChild.SetActive (true);
			commandClip = Microphone.Start (null, false, 10, samplerate);  //Start recording (rewriting older recordings)
		} else {
			// Save the audio file
			m_Image.sprite = recSprite;
			btnRecChild.SetActive (false);
			Microphone.End(null);
			SavWav.Save("sample", commandClip);
			print ("ended");
			// At this point, we can delete the existing audio clip
			//commandClip = null;

			//Grab the most up-to-date JSON file
			// url = "https://api.wit.ai/message?v=20160305&q=Put%20the%20box%20on%20the%20shelf";
			token = "DPAMY5ZZ42WGRITMYY64GYAWJWGME54B";

			//Start a coroutine called "WaitForRequest" with that WWW variable passed in as an argument
			StartCoroutine("WaitForRequest");
		}
	}

	public IEnumerator WaitForRequest(){
		string mypath = Path.Combine(Application.persistentDataPath, "sample.wav");
		//DebugText.text = mypath;
		string witAiResponse = GetJSONText(mypath);
		yield return new WaitForSeconds(0.2f);
		Handle (witAiResponse);
	}
		

	// Update is called once per frame


	string GetJSONText(string file) {

		// get the file w/ FileStream
		FileStream filestream = new FileStream (file, FileMode.Open, FileAccess.Read);
		BinaryReader filereader = new BinaryReader (filestream);
		byte[] BA_AudioFile = filereader.ReadBytes ((Int32)filestream.Length);
		filestream.Close ();
		filereader.Close ();

		// create an HttpWebRequest
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.wit.ai/speech");

		request.Method = "POST";
		request.Headers ["Authorization"] = "Bearer " + token;
		request.ContentType = "audio/wav";
		request.ContentLength = BA_AudioFile.Length;
		request.GetRequestStream ().Write (BA_AudioFile, 0, BA_AudioFile.Length);

		// Process the wit.ai response
		try
		{
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			if (response.StatusCode == HttpStatusCode.OK)
			{
				print("Http went through ok");
				StreamReader response_stream = new StreamReader(response.GetResponseStream());
				return response_stream.ReadToEnd();
			}
			else
			{
				return "Error: " + response.StatusCode.ToString();
				return "HTTP ERROR";
			}
		}
		catch (Exception ex)
		{
			return "Error: " + ex.Message;
			return "HTTP ERROR";
		}

	}

	void Handle(string textToParse) {

		print (textToParse);
		var N = JSON.Parse (textToParse);
		print ("SimpleJSON: " + N.ToString());

		string value = N["entities"][0][0]["value"].Value.ToString ();
		//string value = N["outcomes"] [0] ["entities"]["subject"][0] ["value"].Value.ToLower ();

		print(value);
		//print (value);
		// what function should I call?
		switch (value)
		{
		case "sensei":
			kakashi.GetComponent<AnimatorScript> ().IdleAnswer ();
			break;
		case "imbusy":
			kakashi.GetComponent<AnimatorScript> ().IdleToTalk ();
			break;
		case "howru":
			kakashi.GetComponent<AnimatorScript> ().IdleToHeadNod ();
			break;
		case "shadowclone":
			kakashi.GetComponent<AnimatorScript> ().IdleToBunshin ();
			break;
		case "rasengan":
			kakashi.GetComponent<AnimatorScript> ().IdleToRasengan ();
			break;
		case "chidori":
			kakashi.GetComponent<AnimatorScript> ().IdleToChidori ();
			break;
		case "true":
			kakashi.GetComponent<AnimatorScript> ().IdleToBye ();
			break;
		default:
			print ("Sorry, didn't understand your intent.");
			kakashi.GetComponent<AnimatorScript> ().Bubling ();
			break;
		}
	}


}