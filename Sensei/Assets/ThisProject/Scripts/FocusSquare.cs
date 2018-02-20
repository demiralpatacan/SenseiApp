using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class FocusSquare : MonoBehaviour {

	private bool scenePlaced;
	public GameObject myScene;
	public GameObject GeneratedPlanes;
	public GameObject scaledCam;

	// Use this for initialization
	void Start () {
		scenePlaced = false;
	}

	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {
			var touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				var screenPosition = Camera.main.ScreenToViewportPoint (touch.position);
				ARPoint point = new ARPoint {
					x = screenPosition.x,
					y = screenPosition.y
				};

				List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, 
					                                   ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
				if (hitResults.Count > 0 && scenePlaced == false) {
					foreach (var hitResult in hitResults) {
						scenePlaced=true;
						Vector3 position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
						CamTransformer.ScaledObjectOrigin = position;
						myScene.transform.position = position;
						myScene.transform.eulerAngles = new Vector3 (0f, scaledCam.transform.eulerAngles.y, 0f);
						myScene.GetComponent<Wit3D> ().btnRec.interactable = true;
						//myScene.GetComponent<Wit3D>().ReLocate();
						GeneratedPlanes.SetActive (false);
						myScene.GetComponent<Wit3D> ().ChangeTexture ();
						gameObject.SetActive (false);
						break;
					}
				}
			}
		}
	}
}
