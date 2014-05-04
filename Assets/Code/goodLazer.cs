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

		}

		public void SetColor(Color col)
		{
			lazCol = col;
		}

		void OnCollisionEnter(Collision collision)
		{

			GameObject explosion= new GameObject("Explosion");
			explosion.transform.position = transform.position;
			explosion.gameObject.AddComponent<Detonator> ();
			explosion.gameObject.GetComponent<Detonator>().explodeOnStart = true;
			Destroy(gameObject);
		}
	}
}
