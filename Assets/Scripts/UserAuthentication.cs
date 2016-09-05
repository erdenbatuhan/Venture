//
//  UserAuthentication.cs
//  A Vaila Ball
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class UserAuthentication : MonoBehaviour {
	
	private const string URL_REGISTER_USER = "http://138.68.143.170/VailaBall/r88732gb23g7g3b3u2g1873rb13ubdn131mc218nec811.php";
	private string username = "";
	private string password = "";
	private string message = "";
	public GameObject mainMenu;

	private void OnGUI() {
		if (DatabaseLoader.databaseLoaded && GameMaster.loggedUser == null) {
			GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height - 150, 80, 20), "Username:");
			GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height - 120, 80, 20), "Password:");
			
			username = GUI.TextField(new Rect(Screen.width / 2 - 10, Screen.height - 150, 100, 20), username, 12);
			password = GUI.TextField(new Rect(Screen.width / 2 - 10, Screen.height - 120, 100, 20), password, 12);
			
			if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height - 90, 80, 25), "Login"))
				tryLogin();
			else if (GUI.Button(new Rect(Screen.width / 2 + 5, Screen.height - 90, 80, 25), "Register") && canRegister())
				StartCoroutine(register());
			
			GUI.TextArea(new Rect(30, Screen.height - 45, Screen.width - 60, 25), message);
		} else if (DatabaseLoader.databaseLoaded && GameMaster.loggedUser != null) {
			mainMenu.SetActive(true);
		}
	}

	private void tryLogin() {
		if (isEntryEmpty())
			return;
		
		foreach (User user in GameMaster.users) {
			if (user.getUsername().ToLower() == username.ToLower()) {
				if (user.getPassword() == password) {
					login(user);
					return;
				}
			}
		}
		
		message = GameMaster.CONSOLE_INITIAL + "Your entries are not correct. Please check them again. ";
		resetEntries();
	}
	
	private void login(User user) {
		message = GameMaster.CONSOLE_INITIAL + "Login success!";
		resetEntries();
		GameMaster.loggedUser = user;
	}
	
	private bool canRegister() {
		if (isEntryEmpty())
			return false;
		
		foreach (User user in GameMaster.users) {
			if (user.getUsername().ToLower() == username.ToLower()) {
				message = GameMaster.CONSOLE_INITIAL + "Username already exists. Please choose another one.";
				resetEntries();
				return false;
			}
		}
		
		return true;
	}
	
	private IEnumerator register() {
		WWWForm form = new WWWForm();
		form.AddField("form_username", username);
		form.AddField("form_password", password);
		
		WWW www = new WWW(URL_REGISTER_USER, form);
		message = GameMaster.CONSOLE_INITIAL + "Registering....";
		yield return www; // Wait for the data to be uploaded.

		message = GameMaster.CONSOLE_INITIAL + "Register success!";
		resetEntries();
		
		StartCoroutine(DatabaseLoader.loadDatabase()); // Load the database again.
	}
	
	private bool isEntryEmpty() {
		if (username.Length == 0 || password.Length == 0) {
			message = GameMaster.CONSOLE_INITIAL + "Username or Password cannot be empty.";
			resetEntries();
			return true;
		}
		
		return false;
	}
	
	private void resetEntries() {
		username = "";
		password = "";
	}
}