using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {

	public HealthSystem playerHealthSystem;
	public Shooting playerShootSystem;
	public Text weapon2AmmoAmountText;

	public static HUDController instance;

	public GameObject gameOverMenu;

	// 
	public void LoseGame()
	{
		gameOverMenu.SetActive(true);

	}
		
	// Use this for initialization
	void Start () 
	{
		HUDController.instance = this;
		gameOverMenu.SetActive(false);
	}

	public void Reset()
	{
		gameOverMenu.SetActive (false);
	}

	// Update is called once per frame
	void Update () 
	{
		if(this.playerHealthSystem == null || this.playerShootSystem == null)
		{
			return;
		}
		this.UpdateHealthHUD();
		UpdateAmmoHUD();
	}

	void UpdateAmmoHUD()
	{
		/*this.weapon2AmmoAmountText.text = playerShootSystem.secondaryWeapon.currentAmmoInClip
		+ "/"
		+ playerShootSystem.secondaryWeapon.currentAmmoTotal;*/
		weapon2AmmoAmountText.text = playerShootSystem.secondaryWeapon.currentAmmoInClip +" / "+
			playerShootSystem.secondaryWeapon.maxAmmoTotal;
	}

	public Transform HealthBar;
	public Sprite HealthBlockFull;
	public Sprite HealthBlockEmpty;
	public void UpdateHealthHUD()
	{
		Transform [] Bars;
		Bars = HealthBar.GetComponentsInChildren<Transform>();
		/* First in array is Parent, so starting from 1 which is the first child */
		for(int i = 1; i <= playerHealthSystem.maxHealth; i++)
		{
			Bars[i].GetComponent<Image>().sprite = HealthBlockEmpty;
		}
		for(int i = 1; i <= playerHealthSystem.GetHP(); i++)
		{
			Bars[i].GetComponent<Image>().sprite = HealthBlockFull;
		}
	}
}
