using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class BulletController : MonoBehaviour {

	public float damage { 
		set;
		get;
	}
		
	void OnCollisionEnter(Collision collision) {
		Vector3 collisionPosition = collision.transform.position;

		Physics.OverlapSphere(collisionPosition, 5f)
			.Select(hit => hit.GetComponent<Rigidbody>())
			.Where(rg => rg != null)
			.ToList()
			.ForEach(rg => this.AddExplosionForce(rg, collisionPosition));
				
	}

	private void AddExplosionForce(Rigidbody rg, Vector3 collisionPosition) {
		rg.AddExplosionForce(damage, collisionPosition, 5f, 3.0F);
		Debug.Log ("Boom: " + rg.name);
	}

}
