using UnityEngine;
using System.Collections;

// aim with mouse and turn the 'turret' to look at pointed world position
public class MouseAim : MonoBehaviour 
{
	public Turnable turret;	// turnable turret to command with mouse

    // 
    void Update () 
	{
		// luodaan ray hiiren sijainnista kameralla kohti maailmaa
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		// plane that receives the raycast hits
		Plane hitPlane = new Plane(Vector3.up, Vector3.zero);
        float dist;

		// palauttaa hit:n arvossa tiedot, mihin ray osui
		if(hitPlane.Raycast(ray, out dist))
		{
			// detect world point under mouse cursor
			Vector3 worldPoint = ray.GetPoint(dist);
            // tell turret to turn to look that position
			turret.SetLookTarget(worldPoint);
        }
    }
}
