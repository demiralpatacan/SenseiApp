using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public GameObject witObj;

	bool ispressed = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (!ispressed)
			return;
		// DO SOMETHING HERE
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		ispressed = true;
		//witObj.GetComponent<Wit3D> ().StartRecording ();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		ispressed = false;
		//witObj.GetComponent<Wit3D> ().EndRecording ();
	}
}
