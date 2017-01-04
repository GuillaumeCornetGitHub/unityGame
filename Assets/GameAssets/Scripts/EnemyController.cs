using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	public Transform playerTransform;
	public float XSensitivity = 1.2f; 
	public float moveForce = 10f;

	private Quaternion m_CharacterTargetRot;
	private Transform _transform;
	private WeaponController _weaponController;
	private EnergyController _energyController;
	private Rigidbody _rigidbody;
	private Collider hitCollider;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody> ();
		_transform = GetComponent<Transform> ();
		_weaponController = GetComponentInChildren<WeaponController> ();
		_energyController = GetComponent<EnergyController> ();
	}
	void Update() {
		updateWeapon ();
		updateRotation();
	}

	// Update is called once per frame
	void FixedUpdate() {
		updateMove();
	}

	void updateRotation() {
		_transform.LookAt (playerTransform.position);
	}

	void updateMove() {
		Vector3 force = transform.forward * moveForce * Time.fixedDeltaTime;
		_rigidbody.AddForce (force, ForceMode.Force);
	}

	void updateWeapon() {

		RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			if (hit.collider.tag.Equals ("Player")) {
				
				this.hitCollider = hit.collider;
				Gizmos.color = Color.green;
				try {
					_energyController.consumeEnergy (_weaponController.energyCost);
					_weaponController.fire ();
					_rigidbody.AddForce (
						_weaponController.forceRecoil * -transform.forward,
						ForceMode.Impulse
					);
				} catch (EnergyException ex) {
					Debug.Log (ex);
				}
			} 
		}

		this.hitCollider = null;
	}

	void OnDrawGizmosSelected() {
		if (hitCollider != null) {
			Gizmos.color = Color.blue;
			Gizmos.DrawRay (transform.position, hitCollider.transform.position);
		} else {
			Gizmos.color = Color.red;
			Gizmos.DrawRay (transform.position, transform.forward);
		}
	}


}
