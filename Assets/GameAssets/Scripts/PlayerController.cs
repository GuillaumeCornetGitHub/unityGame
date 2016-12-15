using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class PlayerController : MonoBehaviour {

	public float XSensitivity = 1.2f; 
	public float torqueForce = 10f;
	public float brakeTorqueForce = 10f;

	private Quaternion m_CharacterTargetRot;
	private Transform _transform;
	private WeaponController _weaponController;
	private EnergyController _energyController;
	private float rotateAction;
	private Rigidbody _rigidbody;

	public float moveForce = 1000f;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody> ();
		_transform = GetComponent<Transform> ();
		_weaponController = GetComponentInChildren<WeaponController> ();
		_energyController = GetComponent<EnergyController> ();
	}

	void Update() {
		updateWeapon (CrossPlatformInputManager.GetButtonDown ("Fire1"));
	}
		
	// Update is called once per frame
	void FixedUpdate() {
		updateRotation(CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity);
		updateMove(CrossPlatformInputManager.GetButton("Move"));
	}

	void updateRotation(float rotateX) {
		Quaternion  transformTargetRot = Quaternion.Euler(0f, rotateX * XSensitivity, 0f);
		transform.localRotation *= transformTargetRot;
	}

	void updateMove(bool inputForward) {
		if (inputForward) {
			Vector3 force = transform.forward * moveForce * Time.fixedDeltaTime;
			_rigidbody.AddForce (force, ForceMode.Force);
		}
	}

	void updateWeapon(bool fire) {
		if (fire) {
			try {
				_energyController.consumeEnergy (_weaponController.energyCost);
				_weaponController.fire ();
			} catch (EnergyException ex) {
				Debug.Log (ex);
			}
		}
	}




}
