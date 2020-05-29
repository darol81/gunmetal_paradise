using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	private bool paused = false;
	public GameObject pauseMenu;

	// Use this for initialization
	void Start () {
		this.SetPauseGame (false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			this.TogglePause ();
		}
	}

	// 
	void TogglePause()
	{
		this.SetPauseGame (!paused);
	}

	// 
	public void SetPauseGame(bool pauseState)
	{
		this.paused = pauseState;
		// hide/show pausemenu
		pauseMenu.SetActive (this.paused);
		// enable/disable user input
		PlayerInput.SetActive(!this.paused);
		// mute/unmute audio
		AudioController.instance.SetPauseState(this.paused);
		// pause time
		if (paused) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}
	}

	//
	public void ReturnToMainMenu()
	{
		pauseMenu.SetActive (false);
		Destroy (this.gameObject);
		UnityEngine.SceneManagement.SceneManager.LoadScene ("MainMenuScene");
	}

	//
	public void Restart()
	{
		Time.timeScale = 1f;
		HUDController.instance.Reset ();
		UnityEngine.SceneManagement.SceneManager.LoadScene ("PlayScene");
	}

}
