using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using WebSocketSharp;
using System;

public class LobbyManagerBck : MonoBehaviour {

	public WebSocket ws = new WebSocket("ws://106.14.226.103:8282");

    public readonly Queue<string> msgs = new Queue<string>();

	// Use this for initialization
	void Start () {
		ws.Connect();
		Message mes = new Message();
		ws.OnMessage += (sender, e) =>
		{
            Debug.Log("message: " + e.Data);
            msgs.Enqueue(e.Data);
		};

	}
	
	// Update is called once per frame
	void Update (){
		Debug.Log("size: " + msgs.Count);
		if (msgs.Count > 0) {
			String msg = msgs.Dequeue();
			Message mes = LitJson.JsonMapper.ToObject<Message>(msg);
			switch (mes.message_id)
			{
			  case "SDK_SERVER_MESSAGE_LOGIN_SUCCESS_RESPONSE":
                    Debug.Log("Thread name: " + Thread.CurrentThread.Name);
			      //Login(mes);
			      break;
			  case "MAHJONG_USER_SERVER_MESSAGE_CONNECT_SUCCESS_RESPONSE":
                    Debug.Log("begin");
					//Begin(mes);
					break;
			}
		}
	}

}

