//
//  Enemy.cs
//  Venture
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	private void OnTriggerEnter() {
		GameMaster.restartLevel();
	}
}
