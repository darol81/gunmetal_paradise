using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
	public float maxHealth = 10f;
    private float currentHealth;
	public bool alive = true;

    void Awake()
    {
        currentHealth = maxHealth;
	}

	void Update()
	{
		if (this.alive) 
		{
			if(this.currentHealth <= 0f) 
			{
				this.alive = false;
				Debug.Log (this.gameObject.name + " HealthSystem is DEAD!");
			}
		}
	}

    public bool IsAlive()
    {
        return alive; 
    }

	public void TakeDamage(float amount)
	{
		this.currentHealth -= Mathf.Abs(amount);
	}

	public float GetHP()
	{
		return this.currentHealth;
	}


}
