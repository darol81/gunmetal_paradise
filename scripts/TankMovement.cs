using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Rigidbody) )]
[RequireComponent( typeof(AudioSource) )]
public class TankMovement : MonoBehaviour {
	
	[Header("Tank movement")]
	public GameObject hull;			// HULL Gameobject that moves and turns (the tanks main frame)
	public float acceleration = 2500F;	// how fast is the max tank speed
	public float turnSpeed = 2.5f;	// how fast the tankl can turn
	public float maxVelocity = 3000f;


	private Vector3 moveDirection = Vector3.zero;
	private bool moving;
	private float moveAmt = 0f;		// how much we turn during this frame
	private AudioSource audio;
	private Rigidbody rigid;

	// 
	void Awake()
	{
		this.audio = this.GetComponent<AudioSource> ();
		this.rigid = this.GetComponent<Rigidbody>();
	}

	//
	public void SetMoveDir(Vector3 tgtDir)
	{
		this.moveDirection = tgtDir.normalized;
	}

	//
	void Update() {
		if(moveDirection.magnitude > 0)
		{
			moving = true;
		}
		else
		{
			moving = false;
		}

		float dir = 1;		// reverse or forward? -1 , 1
		// ACTUAL MOVEMENT
		if(moving)
		{	
			// calculate angle between current movedir and desired movedir
			float turnPhase = Vector3.Angle(hull.transform.forward,moveDirection)/180f;

			if(turnPhase > 0.6f)
			{
				dir = -1;
			}
			else if(turnPhase <= 0.4f)
			{
				dir = 1;
			}
			// define move Amt
			float targetMoveAmt = dir * acceleration;
			moveAmt = Mathf.Lerp (moveAmt, targetMoveAmt, 0.25f);

			// define turn amount
			float turnAmt = this.turnSpeed * Time.deltaTime;

			// ROTATE HULL
			Vector3 newDir = Vector3.RotateTowards(transform.forward, dir*moveDirection, turnAmt, 0.0F);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
		// NOT ACCELERATING
		else
		{
			moveAmt *= 0.90f;	// slow down, if not accelerating	
		}

		// MOVE HULL
		rigid.AddForce(hull.transform.forward * moveAmt * Time.deltaTime);
		if(rigid.velocity.magnitude >= this.maxVelocity)
		{
			rigid.velocity = rigid.velocity.normalized * this.maxVelocity;
		}

		this.UpdateMoveSound ();
	}

	//
	private void UpdateMoveSound()
	{
		this.audio.pitch = 0.5f + ( 0.7f * (Mathf.Abs(moveAmt)/acceleration));
	}
}
