using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	private void Start()
	{
		c = Camera.main;
		t = c.transform;
		x = (maintainXZRot ? base.transform.eulerAngles.x : 0f);
		z = (maintainXZRot ? base.transform.eulerAngles.z : 0f);
	}

	private void OnBecameVisible()
	{
		SetRotation();
	}

	private void OnWillRenderObject()
	{
		SetRotation();
	}

	private void SetRotation()
	{
		//base.transform.LookAt(base.transform.position + t.rotation * Vector3.forward);
		//base.transform.rotation = Quaternion.Euler(0f, t.eulerAngles.y, 0f);
		if (c == null)
		{
			return;
		}
		base.transform.rotation = (maintainXZRot ? Quaternion.Euler(x, t.eulerAngles.y, z) : new Quaternion(x, t.rotation.y, z, t.rotation.w));
	}

	public bool maintainXZRot = false;

	private Camera c;

	private Transform t;

	[NonSerialized]
	public float x;

	[NonSerialized]
	public float z;
}
