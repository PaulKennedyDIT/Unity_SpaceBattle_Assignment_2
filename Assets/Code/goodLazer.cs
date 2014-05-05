using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE
{
	class goodLazer:MonoBehaviour
	{
		public Color lazCol;

		public void Start()
		{
			gameObject.AddComponent<Rigidbody> ();
			rigidbody.useGravity = false;
			gameObject.AddComponent<BoxCollider> ();
		}

		public void Update()
		{
			float speed = 5.0f;
			float width = 500;
			float height = 500;

			transform.position += transform.forward * speed;
			LineDrawer.DrawLine(transform.position, transform.position + transform.forward * 5.0f, lazCol);
			DelayedDestroy ();
		}

		public void SetColor(Color col)
		{
			lazCol = col;
		}

		public void DelayedDestroy()
		{
			Destroy (gameObject, 3.5f);
		}
		void OnCollisionEnter(Collision collision)
		{
			GameObject shroom = (GameObject)MonoBehaviour.Instantiate(Resources.Load("Detonator-Base"));
			shroom.transform.position =  transform.position;
			Destroy(gameObject);
		}
	}
}
