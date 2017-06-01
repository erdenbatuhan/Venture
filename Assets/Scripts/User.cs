//
//  User.cs
//  Venture
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class User {

	private int id = 0;
	private string username = null;
	private string password = null;
	private int levelsCompleted = 0;
	private int skin = 0;

	public User(int id, string username, string password, int levelsCompleted, int skin) {
		this.id = id;
		this.username = username;
		this.password = password;
		this.levelsCompleted = levelsCompleted;
		this.skin = skin;
	}

	public override string ToString() {
		return "ID: " + id + " Username: " + username + " Password: " + password + " LevelsCompleted: " + levelsCompleted + " Skin: " + skin;
	}

	/* ----------------------- GETTERS & SETTERS ----------------------- */

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public string getUsername() {
		return username;
	}

	public void setUsername(string username) {
		this.username = username;
	}

	public string getPassword() {
		return password;
	}

	public void setPassword(string password) {
		this.password = password;
	}

	public int getLevelsCompleted() {
		return levelsCompleted;
	}

	public void setLevelsCompleted(int levelsCompleted) {
		this.levelsCompleted = levelsCompleted;
	}

	public int getSkin() {
		return skin;
	}

	public void setSkin(int skin) {
		this.skin = skin;
	}
}
