using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float destroyDelay = 2.0f;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, destroyDelay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
