using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;

public class PlayerController : GameElement, Damageable {

	public float XSensitivity = 1.2f; 
	public float torqueForce = 10f;
	public float moveForce = 1000f;
    public float life = 100;

	private Transform _transform;
	private WeaponController _weaponController;
	private EnergyController _energyController;
	private Rigidbody _rigidbody;


	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody> ();
		_transform = GetComponent<Transform> ();
		_weaponController = GetComponentInChildren<WeaponController> ();
		_energyController = GetComponent<EnergyController> ();
        Cursor.lockState = CursorLockMode.Locked;
    }

	void Update() {
		updateWeapon (CrossPlatformInputManager.GetButtonDown ("Fire1"));
	}
		
	// Update is called once per frame
	void FixedUpdate() {
		updateRotation(CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity);
		updateMove();
	}

	void updateRotation(float rotateX) {
		Quaternion  transformTargetRot = Quaternion.Euler(0f, rotateX * XSensitivity, 0f);
		transform.localRotation *= transformTargetRot;
	}

	void updateMove() {
		if (CrossPlatformInputManager.GetButton("MoveForward")) {
            applyForce(transform.forward);
        }
        if (CrossPlatformInputManager.GetButton("MoveBackward")) {
            applyForce(-transform.forward);
        }
        if (CrossPlatformInputManager.GetButton("MoveToRight")){
            applyForce(transform.right);
        }
        if (CrossPlatformInputManager.GetButton("MoveToLeft")){
            applyForce(-transform.right);
        }
    }

    private void applyForce(Vector3 forceAxis) {
        Vector3 force = forceAxis * moveForce * Time.fixedDeltaTime;
        _rigidbody.AddForce(force, ForceMode.Force);
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

    public override void kill()
    {
       
    }
}
