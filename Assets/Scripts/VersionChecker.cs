//
//  VersionChecker.cs
//  Venture
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class VersionChecker : MonoBehaviour {

	private const string URL_VERSION = "http://138.68.143.170/VailaBall_DATA/Computer_DATA/v2148921ub21f21nf12m1920j129.txt";
	private const string URL_UPDATE = "http://vailaball.me/Download";
	private const string VERSION = "1.0.2";
	private string[] websitesToTest = { "http://www.google.com", "http://www.ebay.com" };
	private bool isInternetConnectionStable;
	private bool isCheckingVersion;
	private bool isUpToDate;
	public GameObject[] gameObjects;

	private void Start() {
		isInternetConnectionStable = true;
		isCheckingVersion = true;
		isUpToDate = true;

		startChecking();
	}

	private void startChecking() {
		StartCoroutine(checkInternetConnection());
	}

	private IEnumerator checkInternetConnection() {
		WWW www = new WWW("http://www.google.com");
		yield return www;

		for (int i = 0; www.error != null; i++) {
			isInternetConnectionStable = false;

			www = new WWW(websitesToTest[i % 2]);
			yield return www;
		}

		isInternetConnectionStable = true;
		StartCoroutine(checkVersion());
	}
	
	private IEnumerator checkVersion() {
		WWW www = new WWW(URL_VERSION);
		yield return www;

		isCheckingVersion = false;

		if (VERSION != getVersion(www.text))
			isUpToDate = false;
		else
			enableGameObjects();
	}

	private string getVersion(string text) {
		string version = text.Substring(text.IndexOf('>') + 1, 5);

		return version;
	}                    

	private void enableGameObjects() {
		foreach (GameObject gameObject in gameObjects)
			gameObject.SetActive(true);
	}
	
	private void OnGUI() {
		if (!isInternetConnectionStable) {
			GUI.TextArea(new Rect(30, Screen.height - 45, Screen.width - 60, 25), GameMaster.CONSOLE_INITIAL + "Please check your internet connection....");
		} else if (isCheckingVersion) {
			GUI.TextArea(new Rect(30, Screen.height - 45, Screen.width - 60, 25), GameMaster.CONSOLE_INITIAL + "Checking version....");
		} else if (!isUpToDate) {
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 135, 100, 25), "Update"))
				Application.OpenURL(URL_UPDATE);
			else if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 25), "Exit"))
				Application.Quit();

			GUI.TextArea(new Rect(30, Screen.height - 45, Screen.width - 60, 25), GameMaster.CONSOLE_INITIAL + "Please update your game to continue.");
		}
	}
}
