using UnityEngine;
using System.Collections;

public class EnemyAim : MonoBehaviour {

	public Turnable turret;
	private Transform player;

	// 
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// 
	void Update ()
	{
		this.turret.SetLookTarget (player.position);
	}
}
