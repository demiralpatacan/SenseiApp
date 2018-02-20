using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class CamTransformer : MonoBehaviour
{

    public Camera NormalCamera;
    public Camera ArCamera;
    private float _cameraScale = 0.25f;
    public static Vector3 ScaledObjectOrigin;
    //private bool _sync = false;


    void LateUpdate()
    {
        Matrix4x4 matrix = UnityARSessionNativeInterface.GetARSessionNativeInterface().GetCameraPose();
        float invScale = 1.0f / _cameraScale;
        Vector3 cameraPos = UnityARMatrixOps.GetPosition(matrix);
        Vector3 vecAnchorToCamera = cameraPos - ScaledObjectOrigin;
        NormalCamera.transform.localPosition = ScaledObjectOrigin + (vecAnchorToCamera * invScale);
        NormalCamera.transform.localRotation = UnityARMatrixOps.GetRotation(matrix);


        //this needs to be adjusted for near/far
        NormalCamera.projectionMatrix = UnityARSessionNativeInterface.GetARSessionNativeInterface().GetCameraProjection();

    }
		
}
