using UnityEngine;
using System.Collections;

public class ExplosionBehaviour : MonoBehaviour {

	public float explosionForce;
	private Rigidbody[] fragments;
	// Use this for initialization
	void Start () {
		fragments = this.transform.GetComponentsInChildren<Rigidbody> ();
		foreach(Rigidbody f in fragments)
		{
			f.AddExplosionForce (Random.Range(explosionForce-50, explosionForce+50), transform.position, 10f);
			f.gameObject.AddComponent<AutoDestroy> ().destroyDelay = Random.Range (10f,15f);
		}
	}

}
