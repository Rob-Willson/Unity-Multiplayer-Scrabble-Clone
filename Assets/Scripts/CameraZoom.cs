﻿using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public static CameraZoom instance = null;

    [SerializeField] public float minZoom = 10f;
    [SerializeField] public float maxZoom = 30f;
    [SerializeField] private float zoomSpeed = 1000f;
    private float targetZoom = 20f;
    private float zoomCurrentVelocity;

    private Transform tr;
    private Transform Tr
    {
        get
        {
            if(tr == null)
            {
                tr = transform;
            }
            return tr;
        }
    }

    private void Awake ()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            Debug.LogError("Duplicate instances of '" + this.GetType().ToString() + "'. Bug?");
        }
        else
        {
            instance = this;
        }
    }

    private void Update ()
    {
        if(Input.GetKey(KeyCode.PageUp))
        {
            SetTargetZoom(1f);
        }
        if(Input.GetKey(KeyCode.PageDown))
        {
            SetTargetZoom(-1f);
        }

        float mouseScrollWheelInputData = Input.GetAxis("Mouse ScrollWheel");
        if(mouseScrollWheelInputData != 0)
        {
            SetTargetZoom(-mouseScrollWheelInputData * 15f);
        }

        if(Tr.localPosition.y == targetZoom)
        {
            return;
        }

        if(Calculations.Approximately(Tr.localPosition.y, targetZoom, 0.1f))
        {
            Tr.localPosition = new Vector3(Tr.localPosition.x, targetZoom, Tr.localPosition.z);
            return;
        }

        float newZoomThisFrame = Mathf.SmoothDamp(Tr.localPosition.y, targetZoom, ref zoomCurrentVelocity, 0.1f);
        Tr.localPosition = new Vector3(Tr.localPosition.x, newZoomThisFrame, Tr.localPosition.z);
    }

    private void SetTargetZoom (float zoomAmount)
    {
        float zoomModifier = zoomAmount * zoomSpeed * Time.unscaledDeltaTime;
        targetZoom =  Mathf.Clamp(Tr.position.y + zoomModifier, minZoom, maxZoom);
    }

}
