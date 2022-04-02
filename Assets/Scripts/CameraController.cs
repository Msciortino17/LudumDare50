using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
	public CinemachineVirtualCamera CMvCAM;

	[Header("Zooming")]
	public float ZoomRate;
	public float MaxZoom;
	public float MinZoom;

	[Header("Panning")]
	public float PanSpeed;
	public float PanModifier;
	private bool panning;
	private Vector3 prevMousePos;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		UpdateZoom();
		UpdatePanningKeys();
		UpdatePanningMouse();
	}

	private void UpdateZoom()
	{
		float delta = Input.mouseScrollDelta.y;
		if (delta > 0f)
		{
			// Zoom in
			float size = CMvCAM.m_Lens.OrthographicSize;
			size -= ZoomRate;
			if (size < MinZoom)
				size = MinZoom;
			CMvCAM.m_Lens.OrthographicSize = size;
		}
		else if (delta < 0f)
		{
			// Zoom out
			float size = CMvCAM.m_Lens.OrthographicSize;
			size += ZoomRate;
			if (size > MaxZoom)
				size = MaxZoom;
			CMvCAM.m_Lens.OrthographicSize = size;
		}
	}

	private void UpdatePanningKeys()
	{
		float zoom = CMvCAM.m_Lens.OrthographicSize;
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			transform.Translate(-PanSpeed * zoom * Time.fixedDeltaTime, 0f, 0f);
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			transform.Translate(PanSpeed * zoom * Time.fixedDeltaTime, 0f, 0f);
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
			transform.Translate(0f, PanSpeed * zoom * Time.fixedDeltaTime, 0f);
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
			transform.Translate(0f, -PanSpeed * zoom * Time.fixedDeltaTime, 0f);
	}

	private void UpdatePanningMouse()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			panning = true;
			prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		if (panning && !Input.GetKey(KeyCode.Mouse0))
		{
			panning = false;
		}

		if (panning)
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);

			Vector3 mouseDelta = mousePos - prevMousePos;
			if (mouseDelta.magnitude > 0.5f)
				transform.Translate(-mouseDelta);
		}
	}
}
