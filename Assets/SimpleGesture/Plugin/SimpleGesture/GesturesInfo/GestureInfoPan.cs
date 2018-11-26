using UnityEngine;
using System.Collections;

public class GestureInfoPan 
{
	public Vector2 deltaDirection {get; private set;}

	public GestureInfoPan(Vector2 deltaDirection)
	{
		this.deltaDirection = deltaDirection;
	}
}
