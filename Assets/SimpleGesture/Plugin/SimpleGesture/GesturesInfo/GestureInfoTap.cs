using UnityEngine;
using System.Collections;

public class GestureInfoTap
{
	public Vector2 position {get; private set; }
	public float duration {get; private set; }

	public GestureInfoTap(Vector2 position, float duration)
	{
		this.position = position;
		this.duration = duration;
	}
}
