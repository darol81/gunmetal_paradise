using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour
{	
	[SerializeField]
	private string damagedTag = "Enemy";
	private float lifeTime = 3.0f;

	public GameObject explosionPrefab;	// what explosion to spawn when this is destroyed
	public float damageAmount = 50f;	// how much damage is dealt to target HealthSystems

	// 
	void Update()
	{
		this.lifeTime -= Time.deltaTime;
		if(this.lifeTime <= 0f)
		{
			Destroy(this.gameObject);
		}
	}

	// Set tag that is reqired to make damage
	public void SetDamageTag(string tag)
	{
		this.damagedTag = tag;
	}

	// 
	void OnTriggerEnter(Collider obj)
	{
		//Debug.Log("Hit: " + obj.gameObject.name);
		if(obj.gameObject.CompareTag(this.damagedTag))
		{
			// DEAL DAMAGE to target
			HealthSystem tgt = obj.gameObject.GetComponent<HealthSystem>();
			tgt.TakeDamage(this.damageAmount);
			// remove this bullet from game
			this.Kill();
		}
		else if(obj.gameObject.tag == "Wall" || obj.gameObject.tag == "Default")
		{
			// remove this bullet from game
			this.Kill();
		}
	}

	// What happens when this bullet is hit or destroyed
	private void Kill()
	{
		// spawn explosion effect
		if(this.explosionPrefab)
		{
			Instantiate (this.explosionPrefab, transform.position, transform.rotation);
		}
		// remove this bullet from game
		Destroy(this.gameObject);
	}
}
