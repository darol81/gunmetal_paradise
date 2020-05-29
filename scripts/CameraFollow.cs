using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public Transform followTarget;
	public float smoothing = 0.1f;

	public float velocityAnticipation = 1.5f;
	private Vector3 tgtPos;

	// Use this for initialization
	void Start ()
    {
		if (this.followTarget == null) {
			this.followTarget = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		tgtPos = followTarget.position + (followTarget.GetComponent<Rigidbody>().velocity * velocityAnticipation);
		transform.position = Vector3.Lerp (transform.position, tgtPos, smoothing);
	}
}
