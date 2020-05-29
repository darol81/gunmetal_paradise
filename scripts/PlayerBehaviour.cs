using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
	//public GameObject hud;

	private bool lost = false;

	// Use this for initialization
	void Start () {
		HUDController.instance.playerHealthSystem = this.GetComponent<HealthSystem> ();
		HUDController.instance.playerShootSystem = this.GetComponent<Shooting> ();

		//Instantiate (this.hud, Vector3.zero, Quaternion.identity);
		HUDController.instance.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		if(this.GetComponent<HealthSystem> ().alive == false && !lost)
		{
			lost = true;
			Time.timeScale = 0f;
			HUDController.instance.LoseGame();
		}
	}

	//
	public float GetHP()
	{
		return GetComponent<HealthSystem> ().GetHP ();
	}
}
