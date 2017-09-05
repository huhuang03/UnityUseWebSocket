using UnityEngine;
using WebSocketSharp;

public class MainScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var ws = new WebSocket("ws://106.14.226.103:8282");
		ws.OnMessage += (sender, e) =>
		{
			print("111");
			print(e.Data);
			ServerData data = LitJson.JsonMapper.ToObject<ServerData>(e.Data);
			print(data.message_id);
			print(data.success_code);
		};
		print("22");

		ws.Connect();
		ws.Send("hello");
		print("333");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	class ServerData
	{
		public string message_id;
		public string success_code;
	}
}
