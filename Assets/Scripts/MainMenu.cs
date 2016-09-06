//
//  MainMenu.cs
//  A Vaila Ball - Computer
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MainMenu : MonoBehaviour {
	
	private bool isLoadScreen = false;
	private bool isLeaderboards = false;
	public Texture[] levelTextures;

	private void OnGUI() {
		if (isLoadScreen)
			seeLoadScreen();
		else if (isLeaderboards)
			seeLeaderboards();
		else
			seeButtons();

		BackgroundMusic.volume = GUI.HorizontalSlider(new Rect (Screen.width / 2 - 150, 10, 300, 30), BackgroundMusic.volume, 0.0f, 1.0f);
	}

	private void seeButtons() {
		if (GameMaster.loggedUser.getLevelsCompleted() == 0) {
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 170, 100, 25), "Start Game"))
				Application.LoadLevel(1);
		} else {
			if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 170, 100, 25), "Load Game"))
				isLoadScreen = true;
		}

		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 135, 100, 25), "Shop"))
			Debug.Log("Shop!");
		else if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 100, 100, 25), "Leaderboards"))
			isLeaderboards = true;
		else if (GUI.Button(new Rect(Screen.width - 90, 10, 80, 25), "Exit"))
			Application.Quit();
	}

	private void seeLoadScreen() {
		if (GUI.Button(new Rect(Screen.width - 90, 10, 80, 25), "Back"))
			isLoadScreen = false;
		else
			isLeaderboards = false;

		drawLoaderButtons();
	}

	private void drawLoaderButtons() {
		int origin = Screen.width / 28;
		int count = 7;
		int diff = 25;
		int width = (Screen.width - ((count - 1) * 25) - (origin * 2)) / count;
		
		for (int level = 1, y = 50; y < Screen.height - width; y += (width + diff)) {
			for (int i = 0, x = origin; i < count; level++, i++, x += (width + diff)) {
				if (level > GameMaster.loggedUser.getLevelsCompleted() + 1 || level > GameMaster.MAXIMUM_LEVEL)
					return;
				
				if (GUI.Button(new Rect(x, y, width, width), levelTextures[level]))
					Application.LoadLevel(level);
			}
		}
	}

	private void seeLeaderboards() {
		if (GUI.Button(new Rect(Screen.width - 90, 10, 80, 25), "Back"))
			isLeaderboards = false;
		else
			isLoadScreen = false;

		for (int i = 0, y = 50; i < GameMaster.users.Count && y < Screen.height - 50; i++, y += 35) {
			string userInformation = (i + 1) + ". " + GameMaster.users[i].getUsername() + " has completed " + GameMaster.users[i].getLevelsCompleted() + " levels.";
			GUI.TextArea(new Rect(50, y, Screen.width - 100, 25), userInformation);
		}
	}
}