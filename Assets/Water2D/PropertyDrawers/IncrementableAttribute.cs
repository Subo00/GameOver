﻿#if UNITY_EDITOR
namespace DynamicLight2D
{
	using UnityEngine;
	using System.Collections;

	public class IncrementableAttribute : PropertyAttribute
	{
		public readonly float incrementBy;
		
		public IncrementableAttribute(float increment = 1.0f)
		{
			this.incrementBy = increment;
		}
	}
}
#endif