using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TestsManager : MonoBehaviour 
{
	// VARIABLES: ------------------------------------------------------------------------------------------------------

	private static TestsManager _instance;
	public Text console;

	// INITIALIZE: -----------------------------------------------------------------------------------------------------

	public void Awake()
	{
		TestsManager._instance = this;

		this.AddMessage("Console");

		SimpleGesture.OnTap(this.CallbackTap);
		SimpleGesture.OnDoubleTap(this.CallbackDoubleTap);

		SimpleGesture.On4AxisSwipeDown(this.CallbackSwipeDown);
		SimpleGesture.On4AxisSwipeUp(this.CallbackSwipeUp);

		SimpleGesture.WhilePanning(this.CallbackPanning);
		SimpleGesture.WhilePinching(this.CallbackPinching);

		SimpleGesture.WhileStretching(this.CallbackStretching);
		SimpleGesture.WhileTwisting(this.CallbackTwisting);
	}

	// PRIVATE METHODS: ------------------------------------------------------------------------------------------------

	private void AddMessage(string message)
	{
		this.console.text = message;
	}

	// CALLBACK METHODS: -----------------------------------------------------------------------------------------------

	public void CallbackTap()
	{
		this.AddMessage("Tap!");
	}

	public void CallbackDoubleTap()
	{
		this.AddMessage("Double Tap!");
	}

	public void CallbackSwipeUp()
	{
		this.AddMessage("Swipe Up!");
	}

	public void CallbackSwipeDown()
	{
		this.AddMessage("Swipe Down!");
	}

	public void CallbackCircle()
	{
		this.AddMessage("Circle!");
	}

	public void CallbackZigZag()
	{
		this.AddMessage("Zig-Zag!");
	}

	public void CallbackPanning(GestureInfoPan pan)
	{
		CameraController._instance.orbitSpeed += pan.deltaDirection.x;
		if (Mathf.Sign(CameraController._instance.orbitSpeed) != Mathf.Sign(pan.deltaDirection.x))
		{
			CameraController._instance.orbitSpeed = pan.deltaDirection.x;
		}

		this.AddMessage("Panning");
	}

	public void CallbackPinching(GestureInfoZoom zoom)
	{
		float fov = CameraController._instance.targetFOV + (zoom.deltaDistance * CameraController._instance.zoomCoeff);
		fov = Mathf.Min(CameraController._instance.maxZoom, fov);
		CameraController._instance.targetFOV = fov;
		this.AddMessage("Pinching");
	}

	public void CallbackStretching(GestureInfoZoom zoom)
	{
		float fov = CameraController._instance.targetFOV - (zoom.deltaDistance * CameraController._instance.zoomCoeff);
		fov = Mathf.Max(CameraController._instance.minZoom, fov);
		CameraController._instance.targetFOV = fov;
		this.AddMessage("Stretching");
	}

	public void CallbackTwisting(GestureInfoTwist twist)
	{
		if (twist.clockwise)
		{
			float rotation = CameraController._instance.targetZRotation + (twist.deltaDistance * CameraController._instance.rotCoeff);
			rotation = Mathf.Min(CameraController._instance.maxRotation, rotation);
			CameraController._instance.targetZRotation = rotation;
			this.AddMessage("Twisting Right");
		}
		else
		{
			float rotation = CameraController._instance.targetZRotation - (twist.deltaDistance * CameraController._instance.rotCoeff);
			rotation = Mathf.Max(CameraController._instance.minRotation, rotation);
			CameraController._instance.targetZRotation = rotation;
			this.AddMessage("Twisting Left");
		}
	}
}
