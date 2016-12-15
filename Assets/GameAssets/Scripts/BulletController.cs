using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private float damage;

	public void setDamage(float damage) {
		this.damage = damage;
	}
	void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.collider);
	}

}
