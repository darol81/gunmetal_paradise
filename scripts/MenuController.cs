using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
	public string inGameScene;
	public static string loadNext;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//
	public void StartGame()
	{
		MenuController.loadNext = inGameScene;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("LoadScene");
	}


	//
	public void QuitGame()
	{
		Application.Quit ();
	}
}
