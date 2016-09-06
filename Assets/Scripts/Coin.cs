//
//  Coin.cs
//  A Vaila Ball - Computer
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public GameObject coinEffect;

	private void OnTriggerEnter() {
		GetComponent<MeshRenderer>().enabled = false;
		GetComponent<BoxCollider>().enabled = false;

		GameMaster.addCoin();
		animate();
	}

	private void animate() {
		GameObject effect = Instantiate(coinEffect);
		effect.transform.position = transform.position;
		GetComponent<AudioSource>().Play();

		destroyChildren();
		Destroy(effect, 3);
		Destroy(gameObject, 3);
	}

	private void destroyChildren() {
		foreach (Transform child in transform)
			Destroy(child.gameObject);
	}
}