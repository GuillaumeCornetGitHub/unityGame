using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	private Transform _transform;
	public Transform _projectileStart;
	public float timeBeforeAutoDestruction = 10.0f;
	public float energyCost;
	public float damage;
	public float forceRecoil;

	public GameObject bullet;


	// Use this for initialization
	void Start () {
		_transform = GetComponent <Transform> ();
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
		currentBullet.GetComponent<BulletController> ().setDamage (this.damage);
		currentBullet.GetComponent<Rigidbody> ().AddForce (_transform.forward * 10, ForceMode.Impulse);
		Destroy (currentBullet, timeBeforeAutoDestruction);
	}

}
