using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class BulletController : MonoBehaviour {

	private Vector3 collisionPosition;
	private float _damage = 100f;
    private Collider _owner;

	public float damage { 
		set {
			this._damage = value;
		}
		get {
			return _damage;
		}
	}

    public Collider owner
    {
        get
        {
            return _owner;
        }

        set
        {
            _owner = value;
        }
    }

    void OnCollisionEnter(Collision collision) {
		if (collision.collider.Equals(owner))
			return;

       Damageable damageableOtional = collision.collider.GetComponent<Damageable>();
        if(damageableOtional != null) {
            damageableOtional.damage(damage);
        }


        collisionPosition = collision.transform.position;

		Physics.OverlapSphere(collisionPosition, 5f)
			.Select(hit => hit.GetComponent<Rigidbody>())
			.Where(rg => rg != null)
			.ToList()
			.ForEach(rg => this.AddExplosionForce(rg, collisionPosition));

		Destroy (this.gameObject);
				
	}

	private void AddExplosionForce(Rigidbody rg, Vector3 collisionPosition) {
		rg.AddExplosionForce(damage, collisionPosition, 5f, 3.0F);
		Debug.Log ("Boom: " + rg.name);
	}
		

}
