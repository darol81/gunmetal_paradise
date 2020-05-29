using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon
{
	[Header("Bullet")]
	public Transform bulletSpawn;
	public GameObject bulletPrefab;
	public float shootForce = 150f; 	// force to shoot the bullet on spawn

	[Header("Sounds")]
	public AudioClip[] shootSounds;
	public AudioClip reloadSound;
	public float initialPitch = 1f;
	public float randomPitchThresold = 0f;

	[Header("Weapon")]
	//public enum ShootMode{SemiAutomatic, Automatic};
	//public ShootMode shootMode;
	public int maxAmmoTotal = 100; 		// 0 == infinite
	public bool infiniteAmmo = true;
	public int currentAmmoTotal = 100;
	public int currentAmmoInClip = 10;
	public int maxAmmoInClip = 10; 	//
	public float reloadSpeed = 2f;		// how long it takes to laod this weapon between clips
	public float shootRate = 0.33f;	// how fast can this weapon fire?

	private bool reloading = false;
	private float reloadPhase = 0f;
	private bool shooting = false;
	private float shootPhase = 0f;

	// call this from shooting scripts update
	public void Process()
	{
		// PROCESS RELOADS
		if (this.reloading)
		{
			this.reloadPhase -= Time.deltaTime;
			if (this.reloadPhase <= 0)
			{
				this.EndReload ();
			}
		}
		else
		{
			if(currentAmmoTotal > 0 && currentAmmoInClip <= 0)
			{
				this.BeginReload ();
			}
		}

		// PROCESS SHOTS
		if (this.shooting)
		{
			this.shootPhase -= Time.deltaTime;
			if (this.shootPhase <= 0)
			{
				this.EndShoot();
			}
		}
	}

	// WHAT HAPPENS WHEN SOMEONE USES THE WEAPONS TRIGGER
	public void Trigger()
	{
		// last shot in progress
		if(this.shooting)
		{
			return;
		}
		// reload in progress
		if (this.reloading)
		{
			return;
		}
		// we have ammo
		if(this.currentAmmoTotal > 0 && this.currentAmmoInClip > 0)
		{
			// SHOOT THE WEAPON!
			this.BeginShoot();
		}
		else
		{
			this.BeginShootEmptyClip();
		}
	}

	// reloading begins
	private void BeginReload()
	{
		this.reloading = true;
		this.reloadPhase = this.reloadSpeed;

		// ReloadSound
		if (this.reloadSound) {
			AudioController.instance.PlaySound(this.reloadSound); // TODO: pitch should not be always randomized!
		}
	}

	// reloading is done
	private void EndReload()
	{
		// ready for next reload
		this.reloading = false;
		this.reloadPhase = 0f;

		// ammo lefg?=
		if(this.currentAmmoTotal > 0)
		{
			int amt = Mathf.Clamp (currentAmmoTotal,1,this.maxAmmoInClip);
			// add ammo to clip from total
			this.currentAmmoInClip = amt;
			//this.currentAmmoTotal -= amt;
		}

	}

	//
	private void BeginShootEmptyClip()
	{
		// delay next shot
		this.shooting = true;
		this.shootPhase = this.shootRate;
		// "click, click" sound
	}

	//
	private void BeginShoot()
	{
		// delay next shot
		this.shooting = true;
		this.shootPhase = this.shootRate;
		// remove ammo from clip
		this.currentAmmoInClip--;
		if (!infiniteAmmo) {
			this.currentAmmoTotal--;
		}
		// spawn bullet
		GameObject bullet = GameObject.Instantiate(this.bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
		// add move force
		bullet.GetComponent<Rigidbody>().AddForce(this.bulletSpawn.forward * this.shootForce, ForceMode.Impulse);
		// play random ShootSound
		if (this.shootSounds.Length > 0) {
			float p = Random.Range(this.initialPitch-randomPitchThresold, this.initialPitch+randomPitchThresold);
			AudioController.instance.PlaySound(this.shootSounds[Random.Range(0,this.shootSounds.Length-1)], p);
		}
	}

	// shot is done
	private void EndShoot()
	{
		// ready for next shot
		this.shooting = false;
		this.shootPhase = 0f;
	}
}
