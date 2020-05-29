using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    Transform player;
    NavMeshAgent nav;

	public GameObject explosionPrefab;
	public GameObject fragmentsPrefab;

	//
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
		if (!player)
		{
			Debug.Log("Player is not tagged as Player!");
			return;
		}
		// INIT NAVMESH
        nav = GetComponent<NavMeshAgent>();
		nav.updatePosition = false;
		nav.updateRotation = false;
		EnemySpawning.instance.aliveChildren++;
	}

	// 
	void Update ()
    {
		//
		this.ProcessNavigation();
		this.ProcessShooting ();

		//
		if(!GetComponent<HealthSystem>().alive)
		{
			if(this.explosionPrefab != null)
			{
				Instantiate (this.explosionPrefab, this.transform.position, this.transform.rotation);
				Instantiate (this.fragmentsPrefab, this.transform.position, this.transform.rotation);
			}
			Destroy(this.gameObject);
		}
	}

	//
	private void ProcessNavigation()
	{
		// test stop at distance
		if(Vector3.Distance(transform.position,player.position) <= 20)
		{
			this.GetComponent<TankMovement>().SetMoveDir(Vector3.zero);
			return;
		}
		nav.nextPosition = transform.position;
		nav.SetDestination(player.transform.position);
		if(nav.hasPath) 
		{
			if (nav.path.corners.Length == 1)
			{
				Vector3 dir = nav.path.corners[0] - transform.position;
				this.GetComponent<TankMovement>().SetMoveDir(dir);
			}
			else
			{
				Vector3 dir = nav.path.corners[1] - nav.path.corners[0];
				this.GetComponent<TankMovement>().SetMoveDir(dir);
			}
		}
	}

	//
	private void ProcessShooting()
	{
		// test stop at distance
		if(Vector3.Distance(transform.position,player.position) <= 25)
		{
			this.GetComponent<EnemyShooting>().Shoot();
		}
	}

	//
	void OnDisable()
	{
		EnemySpawning.instance.aliveChildren--;
	}
}
