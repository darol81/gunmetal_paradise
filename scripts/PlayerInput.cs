using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	private static bool active = true;

	// Update is called once per frame
	void Update () {

		if (!active)
		{
			return;
		}

		// UPDATE TANK MOVEMENT
		this.GetComponent<TankMovement>().SetMoveDir(this.ReadUserInput());


		if(Input.GetMouseButton(0))
		{
			this.GetComponent<Shooting>().mainWeapon.Trigger();
		}
		if(Input.GetMouseButtonDown(1))
		{
			this.GetComponent<Shooting>().secondaryWeapon.Trigger();
		}


	}

	//
	public Vector3 ReadUserInput()
	{
		if (!active)
		{
			return Vector3.zero;
		}
		Vector3 dir;

		// VERTICAL DIR
		Vector3 dirV = Input.GetAxis("Vertical") * Quaternion.Euler(0, Camera.main.transform.up.y, Camera.main.transform.up.z).eulerAngles;	

		// HORIZONTAL DIR
		Vector3 dirH = (Camera.main.transform.right*Input.GetAxis("Horizontal"));

		// V AND H DIR COMBINED
		dir = dirH + dirV;
		dir.y = 0;

		return dir.normalized;
	}

	//
	public static void SetActive(bool state)
	{
		PlayerInput.active = state;
	}
}
