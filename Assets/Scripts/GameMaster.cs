//
//  GameMaster.cs
//  A Vaila Ball
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

	private const int COIN_VALUE = 1;
	public const string CONSOLE_INITIAL = "   ~ Vaila Console: ";
	public const int MAXIMUM_LEVEL = 16;
	public static int currentScore;
	public static int currentLevel;
	public static int coinAmount;
	public static float timer;
	public static User loggedUser;
	public static List<User> users;
	public bool isMenu;

	private void OnGUI() {
		if (loggedUser != null)
			generateUsernameBox();

		if (!isMenu) {
			GUI.Box(new Rect(((Screen.width / 2) - 70), 10, 140, 25), "' " + timer.ToString("0") + " '  Seconds Left");
			GUI.Box(new Rect(((Screen.width / 2) - 70), 45, 140, 25), "Coin Bag: " + currentScore + "/" + coinAmount);
		}
	}

	private void generateUsernameBox() {
		if (loggedUser != null) {
			int length = loggedUser.getUsername().Length;
			int width = (length < 10) ? ((length < 6) ? 50 : 75) : 100;

			GUI.Box(new Rect(10, 10, width, 25), loggedUser.getUsername());
		}
	}

	private void Start() {
		currentScore = 0;
	}

	private void Update() {
		if (!isMenu) {
			checkCoinAmount();
			checkTimer();
		}
	}

	private void checkCoinAmount() {
		if (currentScore >= coinAmount)
			Application.LoadLevel("TransactionScene");
	}

	private void checkTimer() {
		if (timer <= 0)
			restartLevel();
		else
			timer -= Time.deltaTime;
	}

	public static void restartLevel() {
		loadLevel(currentLevel);
	}

	private static void loadLevel(int level) {
		Application.LoadLevel(level % (MAXIMUM_LEVEL + 1));
	}

	public static void addCoin() {
		currentScore += COIN_VALUE;
	}
}