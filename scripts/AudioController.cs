using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

	public static AudioController instance;
	private new AudioSource audio;

	public UnityEngine.Audio.AudioMixer mixer;

	//
	void Awake()
	{
		if(AudioController.instance == null)
		{
			AudioController.instance = this;
		}
		else
		{
			Debug.LogWarning("Theres More than 1 singleton in the Scene!");
			this.enabled = false;
		}
	}

	// Use this for initialization
	void Start () {
		this.audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	//
	public void PlaySound(AudioClip clip)
	{
		audio.clip = clip;
		//audio.pitch = Random.Range(1-randomPitchThresold, 1+randomPitchThresold);
		//audio.volume = Random.Range(1-randomPitchThresold, 1+randomPitchThresold);
		audio.Play();
		//Debug.Log("playing sound");
	}
	//
	public void PlaySound(AudioClip clip, float pitch)
	{
		audio.clip = clip;
		audio.pitch = pitch;
		audio.Play();
		//Debug.Log("playing sound");
	}

	//
	public void SetPauseState(bool state)
	{
		if (state) {
			mixer.FindSnapshot ("Paused").TransitionTo(0f);
		} else {
			mixer.FindSnapshot ("Snapshot").TransitionTo (0f);
		}
	}
}
