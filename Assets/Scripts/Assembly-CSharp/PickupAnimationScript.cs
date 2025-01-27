using System;
using UnityEngine;

public class PickupAnimationScript : MonoBehaviour
{
	private void OnBecameVisible()
	{
		SetAnimationPos();
	}

	private void OnWillRenderObject()
	{
		SetAnimationPos();
	}

	private void SetAnimationPos()
	{
		base.transform.localPosition = new Vector3(0f, (Mathf.Cos(Time.time * speed * 2f) * height / 2f) + 0.9f, 0f);
	}

	public float speed = 1f;

	public float height = 1f;
}
