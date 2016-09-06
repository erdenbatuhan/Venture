//
//  Ball.cs
//  A Vaila Ball - Computer
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	
	private const int SPEED_FORCE = 10;
	private const int JUMP_FORCE = 8;
	private int jumpingsLeft;
	public GameObject camera;
	public GameObject sun;
	public int currentLevel;
	public int coinAmount;
	public float timer;

	private void OnCollisionStay() {
		jumpingsLeft = 2;	
	}

	private void Start() {
		GameMaster.currentLevel = currentLevel;
		GameMaster.coinAmount = coinAmount;
		GameMaster.timer = timer;
	}

	private void Update() {
		move();
		jump();

		trackBall();
		nullifyZ();
	}
	
	private void move() {
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			GetComponent<Rigidbody>().velocity = new Vector2(0, GetComponent<Rigidbody>().velocity.y);
		else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			GetComponent<Rigidbody>().velocity = new Vector2(SPEED_FORCE, GetComponent<Rigidbody>().velocity.y);
		else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			GetComponent<Rigidbody>().velocity = new Vector2(-SPEED_FORCE, GetComponent<Rigidbody>().velocity.y);
	}

	private void jump() {
		if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && --jumpingsLeft > 0) {
			GetComponent<AudioSource>().Play();
			GetComponent<Rigidbody>().velocity = new Vector2(GetComponent<Rigidbody>().velocity.x, JUMP_FORCE);
		}
	}

	private void trackBall() {
		camera.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -10);
		sun.transform.position = new Vector3(transform.position.x, transform.position.y + 10, -5);
	}
	
	private void nullifyZ() {
		if (transform.position.z != 0) // 'z' should be zero (0) all the time.
			transform.position = new Vector3(transform.position.x, transform.position.y, 0); // Make 'z' zero (0).
	}
}