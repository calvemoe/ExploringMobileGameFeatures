using UnityEngine;
using System.Collections;

public class GestureInfoZigZag
{
	public Vector2 direction {get; private set; }
	public float distance {get; private set; }
	
	public GestureInfoZigZag(Vector2 direction, float distance)
	{
		this.direction = direction;
		this.distance = distance;
	}
}
