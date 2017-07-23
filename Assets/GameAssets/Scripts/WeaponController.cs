using UnityEngine;
using System.Collections;
using System;

public class WeaponController : MonoBehaviour {

	private Transform _transform;
	public Transform _projectileStart;
	public float timeBeforeAutoDestruction = 10.0f;
	public float energyCost;
	public float damage;
	public float forceRecoil;

	public GameObject bullet;
    private Collider owner;



    // Use this for initialization
    void Start () {
		_transform = GetComponent <Transform> ();
        owner = GetComponentInParent<Collider>();
  
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void fire(){
		GameObject currentBullet = (GameObject)Instantiate(
			bullet, 
			_projectileStart.position, 
			_transform.rotation
		);

        BulletController bulletController = currentBullet.GetComponent<BulletController>();
        bulletController.damage = this.damage;
        bulletController.owner = this.owner;

		currentBullet.GetComponent<Rigidbody> ().AddForce (
            _transform.forward * 10, ForceMode.Impulse
        );

        addRecoil();

		Destroy (currentBullet, timeBeforeAutoDestruction);
	}

    private void addRecoil() {
        owner.GetComponent<Rigidbody>().AddForce(
            this.forceRecoil * -transform.forward,
            ForceMode.Impulse
        );
    }

}
