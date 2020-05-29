using UnityEngine;
using System.Collections;

public class EnemySpawning : MonoBehaviour
{
    public float spawnTime = 3f;
    public GameObject enemy;
    public Transform[] spawnPoints;

	public int aliveChildren = 0;
	public int maxAliveChildren = 20;

	public static EnemySpawning instance;

	private float lastSpawn;

	void Awake()
	{
		EnemySpawning.instance = this;
		EnemySpawning.instance.aliveChildren = 0;
	}

	// 
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F1))
		{
			this.Spawn ();
		}
		if(EnemySpawning.instance.aliveChildren >= EnemySpawning.instance.maxAliveChildren)
		{
			return;
		}
		this.spawnTime = 5-(Time.timeSinceLevelLoad*0.05f);
		this.spawnTime = Mathf.Clamp (spawnTime, 1, 5);

		if (Time.timeSinceLevelLoad - lastSpawn > spawnTime) {
			this.Spawn ();
		}
	}

	//
    void Spawn()
    {
        int spawnPointIndex;
        spawnPointIndex = Random.Range(0, spawnPoints.Length);
		this.lastSpawn = Time.timeSinceLevelLoad;
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		Debug.Log ("spawning enemy");
    }
}
