using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

public class EnemyController : GameElement {
	
	public Transform playerTransform;
	public float XSensitivity = 1.2f; 
	public float moveForce = 10f;

	private Quaternion m_CharacterTargetRot;
	private Transform _transform;
	private WeaponController _weaponController;
	private EnergyController _energyController;
	private Rigidbody _rigidbody;
    private NavMeshPath _navMeshPath = new NavMeshPath();
    private Collider hitCollider;
	private Vector3 newForceMove;

	private float lastFire;
	private float timeBetweenFire = 1f;
	private float newTime;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody> ();
		_transform = GetComponent<Transform> ();
		_weaponController = GetComponentInChildren<WeaponController> ();
		_energyController = GetComponent<EnergyController> ();
       
      
        lastFire = Time.fixedTime;
	}
	void Update() {
        updateNavMeshAgent();
		updateWeapon ();
		updateRotation();
	}

    private void updateNavMeshAgent()
    {
        NavMesh.CalculatePath(
            _transform.position, 
            playerTransform.position, 
            NavMesh.AllAreas, 
            _navMeshPath
       );
    }

    // Update is called once per frame
    void FixedUpdate() {
		updateMove();
	}

	void updateRotation() {
		_transform.LookAt (_navMeshPath.corners[0]);
	}

	void updateMove() {
		newForceMove = transform.forward * moveForce * Time.fixedDeltaTime;
		_rigidbody.AddForce (newForceMove, ForceMode.Force);
	}

	void updateWeapon() {
		newTime = Time.time;
		if ((Time.time - lastFire) < timeBetweenFire)
			return;

		RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			if (hit.collider.tag.Equals ("Player")) {
				
				this.hitCollider = hit.collider;
				try {
					_energyController.consumeEnergy (_weaponController.energyCost);
					_weaponController.fire ();				
				} catch (EnergyException ex) {
					Debug.Log (ex);
				}

			} 
		}

		lastFire = Time.time;
		this.hitCollider = null;
	}

    public override void kill()
    {
        Destroy(gameObject);
    }
}
