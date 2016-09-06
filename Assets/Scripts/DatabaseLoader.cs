//
//  DatabaseLoader.cs
//  A Vaila Ball - Computer
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DatabaseLoader : MonoBehaviour {

	private const string URL_LOAD_USERS = "http://138.68.143.170/VailaBall_DATA/Computer_DATA/l7237h2347g2387d3b2f7g32b3e7geb2u2g3infn2unz.php";
	public static bool databaseLoaded = false;

	private void OnGUI() {
		if (!databaseLoaded)
			GUI.TextArea(new Rect(30, Screen.height - 45, Screen.width - 60, 25), GameMaster.CONSOLE_INITIAL + "Connecting to database....");
	}
	
	private void Update() {
		// Print users if '@' is pressed.
		if (Input.GetKeyDown(KeyCode.RightShift))
			foreach (User user in GameMaster.users)
				print(user);
		
		// Force database to load if 'F' is pressed.
		if (Input.GetKeyDown(KeyCode.F))
			StartCoroutine(loadDatabase()); // Load the database again.
	}

	private void Start() {
		StartCoroutine(loadDatabase());
	}

	public static IEnumerator loadDatabase() {
		databaseLoaded = false;
		WWW www = new WWW(URL_LOAD_USERS);
		yield return www; // Wait for the data to be downloaded.
		
		string text = www.text;
		extractDataFromDatabase(text.Split(';'));
		databaseLoaded = true;
	}

	private static void extractDataFromDatabase(string[] data) {
		GameMaster.users = new List<User>();

		for (int i = 0; i < data.Length && !data[i].Equals(""); i++) {	
			string[] temp = data[i].Split('|');
			GameMaster.users.Add(new User(int.Parse(temp[0]), temp[1], temp[2], int.Parse(temp[3]), int.Parse(temp[4])));
		}

		GameMaster.users = GameMaster.users.OrderByDescending(user => user.getLevelsCompleted()).ToList();
	}
}