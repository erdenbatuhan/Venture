//
//  DatabaseUpdater.cs
//  A Vaila Ball
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class DatabaseUpdater : MonoBehaviour {

	private const string URL_UPDATE_USERS = "http://138.68.143.170/VailaBall/u82g932buig23bi3n2832ud3b23bf2382db33872.php";
	private bool isUpdatingDatabase = false;

	private void OnGUI() {
		if (isUpdatingDatabase)
			GUI.TextArea(new Rect(30, Screen.height - 45, Screen.width - 60, 25), GameMaster.CONSOLE_INITIAL + "Your progress is being saved to the database....");
	}

	private void Start() {
		updateUser();
	}

	private void updateUser() {
		if (GameMaster.loggedUser.getLevelsCompleted() < GameMaster.currentLevel) {
			isUpdatingDatabase = true;
			GameMaster.loggedUser.setLevelsCompleted(GameMaster.currentLevel++);
			StartCoroutine(updateDatabase());
		} else {		
			Application.LoadLevel(++GameMaster.currentLevel);
		}
	}

	private IEnumerator updateDatabase() {
		WWWForm form = new WWWForm();
		form.AddField("form_id", GameMaster.loggedUser.getId());
		form.AddField("form_username", GameMaster.loggedUser.getUsername());
		form.AddField("form_password", GameMaster.loggedUser.getPassword());
		form.AddField("form_levelsCompleted", GameMaster.loggedUser.getLevelsCompleted());
		form.AddField("form_skin", GameMaster.loggedUser.getSkin());
		
		WWW www = new WWW(URL_UPDATE_USERS, form);
		yield return www; // Wait for the data to be updated.
		
		Application.LoadLevel(GameMaster.currentLevel);
	}
}