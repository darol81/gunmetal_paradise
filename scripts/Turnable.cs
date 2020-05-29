using UnityEngine;
using System.Collections;

public class Turnable : MonoBehaviour {

	[Header("Turning")]
	public float turnSpeedMax = 15f;	// maximi kaantymisnopeus
	public float turnAcceleration = 1;	// 0.. 1 kuinka nopeesti kiihdytetaan kaantymista
	public bool lockY = true;			// only turn around y axis
	private float turnAmt;
	private Vector3 targetDirection;

	[Header("Sounds")]
	public AudioClip turnSoundLoop; // loop this
	private AudioSource audio;

	// Use this for initialization
	void Awake() {
		this.audio = GetComponent<AudioSource> ();
		if(this.audio != null)
		{
			this.audio.clip = this.turnSoundLoop;
			this.audio.loop = true;
			this.audio.volume = 0f;
			this.audio.Play();
		}
	}
	
	// 
	void Update () {
		// always turn to target
		this.ProcessRotation();
		// audio
		this.ProcessSound ();
	}

	// set look target (a world position vector 3)
	public void SetLookTarget(Vector3 worldPos)
	{
		// target dir as Vector3
		this.targetDirection = new Vector3(worldPos.x,transform.position.y,worldPos.z) - transform.position;
		//Quaternion.Euler (0, tgtRot.eulerAngles.y, 0);
	}

	//
	void ProcessRotation()
	{
		// turn speed this frame
		float turnPhase = Vector3.Angle(transform.forward, targetDirection)/180f;
		this.turnAmt = Mathf.Lerp(this.turnAmt, this.turnSpeedMax * turnPhase, this.turnAcceleration);
		this.turnAmt = Mathf.Clamp (this.turnAmt, 0, this.turnSpeedMax);
		// new dir this frame
		Vector3 newDir = Vector3.RotateTowards(transform.forward, this.targetDirection, turnAmt * Time.deltaTime, 0.0F);
		// turn this transform
		Quaternion newRot = Quaternion.LookRotation(newDir);
		newRot.x = 0f;
		newRot.z = 0f;
		transform.rotation = newRot;
	}

	//
	void ProcessSound()
	{
		if(this.audio != null)
		{
			this.audio.volume = EaseOutExpo(0f,1f, this.turnAmt / this.turnSpeedMax,1f);
			this.audio.pitch = this.audio.volume;
		}
	}

	//
	private float EaseOutExpo(float start, float distance, float elapsedTime, float duration)
	{
		// clamp elapsedTime to be <= duration
		if (elapsedTime > duration)
		{
			elapsedTime = duration;
		}
		return distance * ( -Mathf.Pow( 2, -10 * elapsedTime/duration ) + 1 ) + start;
	}
}
