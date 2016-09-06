//
//  MouseListener.cs
//  A Vaila Ball - Computer
//
//  Created by Batuhan Erden.
//  Copyright © 2016 Batuhan Erden. All rights reserved.
//

using UnityEngine;
using System.Collections;

public class MouseListener : MonoBehaviour {

	private void OnMouseEnter(){
		GetComponent<AudioSource>().Play();
		GetComponent<Renderer>().material.color = Color.yellow;
	}
	
	private void OnMouseExit(){
		GetComponent<Renderer>().material.color = Color.white;
	}

	private void OnMouseUp() {
		if (gameObject.name.Equals("M") || gameObject.name.Equals("E") || gameObject.name.Equals("N") || gameObject.name.Equals("U"))
			Application.LoadLevel(0);
	}
}