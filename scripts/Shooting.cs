using UnityEngine;
using System.Collections;

// 
public class Shooting : MonoBehaviour
{
	[Header("Weapons")]
	public Weapon mainWeapon;
	public Weapon secondaryWeapon;
	
	// Update is called once per frame
	protected virtual void Update() 
	{
		this.mainWeapon.Process ();
		this.secondaryWeapon.Process ();
    }
}
