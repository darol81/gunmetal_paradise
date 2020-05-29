using UnityEngine;
using System.Collections;

public class EnemyShooting : Shooting
{
    public float shotDelay = 3f;
    float shootNext;

    void Start ()
    {
        shootNext = Time.time + shotDelay;
	}

    // Update is called once per frame
    protected override void Update()
    {
		this.mainWeapon.Process ();
    }

	//
	public void Shoot()
	{
		this.mainWeapon.Trigger();
	}
}
