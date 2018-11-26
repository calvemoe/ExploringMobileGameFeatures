using UnityEngine;
using System.Collections;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// EXTEND SIMPLE GESTURE: ----------------------------------------------------------------------------------------------

public partial class SimpleGesture
{
	public static void OnDoubleTap(GestureDelegate method) 
	{
		if (GestureDoubleTap._instance == null)
		{
			GestureDoubleTap gestureTap = new GestureDoubleTap();
			SimpleGesture.Instance.oneFingerGestures.Add(gestureTap);
		}

		GestureDoubleTap._instance.AddDelegate(method);
	}

	public static void OnDoubleTap(GestureDelegate<GestureInfoTap> method) 
	{
		if (GestureDoubleTap._instance == null)
		{
			GestureDoubleTap gestureTap = new GestureDoubleTap();
			SimpleGesture.Instance.oneFingerGestures.Add(gestureTap);
		}

		GestureDoubleTap._instance.AddDelegate(method);
	}

	public static void StopDoubleTap(GestureDelegate method)
	{
		GestureDoubleTap._instance.RemoveDelegate(method);

		if (!GestureDoubleTap._instance.HasDelegates())
		{
			SimpleGesture.Instance.oneFingerGestures.Remove(GestureDoubleTap._instance);
			GestureDoubleTap._instance = null;
		}
	}

	public static void StopDoubleTap(GestureDelegate<GestureInfoTap> method)
	{
		GestureDoubleTap._instance.RemoveDelegate(method);

		if (!GestureDoubleTap._instance.HasDelegates())
		{
			SimpleGesture.Instance.oneFingerGestures.Remove(GestureDoubleTap._instance);
			GestureDoubleTap._instance = null;
		}
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// GESTURE: ------------------------------------------------------------------------------------------------------------

public class GestureDoubleTap : BaseGesture 
{
	// VARIABLES: ------------------------------------------------------------------------------------------------------

	public static GestureDoubleTap _instance;

	public const float TAP_MAX_DISTANCE = 25.0f;
	public const float MAX_TIME_BETWEEN_TAPS = 0.5f;

	protected SimpleGesture.GestureDelegate broadcastOnDoubleTap;
	protected SimpleGesture.GestureDelegate<GestureInfoTap> broadcastOnDoubleTapI;

	private float lastTapTime = -1000.0f;

	// CONSTRUCTOR: ----------------------------------------------------------------------------------------------------

	public GestureDoubleTap() : base()
	{
		GestureDoubleTap._instance = this;
	}

	public override void Delete()
	{
		GestureDoubleTap._instance = null;
	}

	// OVERRIDES: ------------------------------------------------------------------------------------------------------

	protected override void OnEnded(TouchInfo touchInfo)
	{
		if (this.IsDoubleTap(touchInfo))
		{
			if (this.broadcastOnDoubleTap  != null) this.broadcastOnDoubleTap();
			if (this.broadcastOnDoubleTapI != null) 
			{
				float duration = Time.time - touchInfo.GetStartTime();
				Vector2 position = touchInfo.GetFirstPosition();
				GestureInfoTap gesture = new GestureInfoTap(position, duration);
				this.broadcastOnDoubleTapI(gesture);
			}
		}
	}

	// ADD AND REMOVE METHODS: -----------------------------------------------------------------------------------------

	public override void AddDelegate(SimpleGesture.GestureDelegate method)
	{
		this.broadcastOnDoubleTap += method;
	}

	public override void RemoveDelegate(SimpleGesture.GestureDelegate method)
	{
		this.broadcastOnDoubleTap -= method;
	}

	public void AddDelegate(SimpleGesture.GestureDelegate<GestureInfoTap> method)
	{
		this.broadcastOnDoubleTapI += method;
	}

	public void RemoveDelegate(SimpleGesture.GestureDelegate<GestureInfoTap> method)
	{
		this.broadcastOnDoubleTapI -= method;
	}

	public override bool HasDelegates()
	{
		bool del1 = (this.broadcastOnDoubleTap  == null ? false : true);
		bool del2 = (this.broadcastOnDoubleTapI == null ? false : true);
		return (del1 || del2);
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// OTHER METHODS: --------------------------------------------------------------------------------------------------

	private bool IsDoubleTap(TouchInfo touchInfo)
	{
		if (touchInfo.GetTotalDistance() < TAP_MAX_DISTANCE)
		{
			if (Time.time <= this.lastTapTime + GestureDoubleTap.MAX_TIME_BETWEEN_TAPS)
			{
				this.lastTapTime = -1000.0f;
				return true;
			}

			this.lastTapTime = Time.time;
		}

		return false;
	}
}
