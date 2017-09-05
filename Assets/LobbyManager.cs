using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class LobbyManager : MonoBehaviour
{

    public GameObject nnBtn;
    public Button SanshuiBtn;
    public Button BaibanBtn;
    public Button SuihaBtn;


    public WebSocket ws = new WebSocket("ws://106.14.226.103:8282");
    public static LobbyManager instance;
    public Sprite sprite;
	private Queue<string> msgs = new Queue<string>();

	public List<string> _list = new List<string>();
    void Awake()
    {
        instance = this;
        //sprite = nnBtn.GetComponent<Image>().sprite;
    }

    void Start()
    {
        ws.Connect();
        Message mes = new Message();
        ws.OnMessage += (sender, e) =>
        {
			msgs.Enqueue(e.Data);
		};
    }


	// Update is called once per frame
	void Update()
	{
		Debug.Log("size: " + msgs.Count);
		if (msgs.Count > 0)
		{
			string msg = msgs.Dequeue();
			Debug.Log(msg);
			Message mes = LitJson.JsonMapper.ToObject<Message>(msg);
			switch (mes.message_id)
			{
				case "SDK_SERVER_MESSAGE_LOGIN_SUCCESS_RESPONSE":
					Login(mes);
					break;
				case "MAHJONG_USER_SERVER_MESSAGE_CONNECT_SUCCESS_RESPONSE":
					Begin(mes);
					break;
			}
		}
	}

	public void Begin(Message mes) //发送
    {
        string m_uuid = mes.uuid.ToString();
        JsonData data = new JsonData();
        data["message_id"] = "SDK_SERVER_MESSAGE_LOGIN_SUCCESS_RESQUEST";

        data["uuid"] = m_uuid;
        string json1 = data.ToJson();
        ws.Send(json1);


        // Debug.Log("这是链接成功");
    }

    public void Login(Message mes)  //接受
    {

        JsonData gameList = JsonMapper.ToObject(mes.game_list);
        for (var i = 0; i < gameList.Count; i++)
        {
            //print(gameList[i]["game_icon"] + "****");
            //print(gameList[i]["game_name"] + "****");
            _list.Add(gameList[i]["game_icon"].ToString());
            string pathName = "Images/" + gameList[i]["game_icon"].ToString();
            sprite = Resources.Load(pathName, typeof(Sprite)) as Sprite;
        }
    }
}