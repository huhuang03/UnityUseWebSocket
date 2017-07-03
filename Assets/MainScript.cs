using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class MainScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var ws = new WebSocketSharp.Server.WebSocketServer ("ws://127.0.0.1:13269");
		ws.AddWebSocketService<AA> ("/");
		ws.Start ();
	}

	class AA : WebSocketSharp.Server.WebSocketBehavior {
		protected override void OnMessage (MessageEventArgs arg) {
			Debug.Log ("On Message: " + arg.Data);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
