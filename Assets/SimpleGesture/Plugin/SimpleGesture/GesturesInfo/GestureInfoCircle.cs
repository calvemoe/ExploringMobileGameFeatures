using UnityEngine;
using System.Collections;

public class GestureInfoCircle
{
	public float radius {get; private set;}
	public Vector2 center {get; private set;}

	public GestureInfoCircle(float radius, Vector2 center)
	{
		this.radius = radius;
		this.center = center;
	}
}
