using System;
using System.Collections.Generic;
using System.Linq;
using Game.script.logic;


public partial class MsgHandler {
	public static List<Player> playersInRoom = new List<Player>();
	public static List<string> playersID = new List<string>();
	public static Dictionary<string, int> mapPlayer = new Dictionary<string, int>();
	private static int debug = 0;
	
	//查询战绩
	public static void MsgGetAchieve(ClientState c, MsgBase msgBase){
		MsgGetAchieve msg = (MsgGetAchieve)msgBase;
		Player player = c.player;
		if(player == null) return;

		msg.win = player.data.win;
		msg.lost = player.data.lost;

		player.Send(msg);
	}


	//请求房间列表
	public static void MsgGetRoomList(ClientState c, MsgBase msgBase){
		MsgGetRoomList msg = (MsgGetRoomList)msgBase;
		Player player = c.player;
		if(player == null) return;

		player.Send(RoomManager.ToMsg());
	}

	//创建房间
	public static void MsgCreateRoom(ClientState c, MsgBase msgBase){
		MsgCreateRoom msg = (MsgCreateRoom)msgBase;
		Player player = c.player;
		if(player == null) return;
		//已经在房间里
		// if(player.roomId >=0 ){
		// 	msg.result = 1;
		// 	player.Send(msg);
		// 	return;
		// }
		//创建
		Room room = RoomManager.AddRoom();
		room.AddPlayer(player.id);

		msg.result = 0;
		player.Send(msg);
	}

	//进入房间
	public static void MsgEnterRoom(ClientState c, MsgBase msgBase)
	{
		Player currentPlayer = c.player;
		if (currentPlayer == null)
		{
			return;
		}
		playersInRoom.Add(currentPlayer);
		playersID.Add(currentPlayer.id);
		MsgEnterRoom msg = (MsgEnterRoom)msgBase;
		msg.result = 1;
		msg.playerID = playersID;
		foreach (Player player in playersInRoom)
		{
			player.Send(msg);
		}
		if (playersInRoom.Count == 3)
		{
			string[] sendList = new string[3]{"", "", ""};
			serverCreater sc = new serverCreater();
			sc.CreateCard();
			sc.Shuffle();
			int cardCnt = 0;
			foreach (cardData data in sc.dealcards)
			{
				if (cardCnt >= 51)
				{
					sendList[0] += data.ToString();
					sendList[1] += data.ToString();
					sendList[2] += data.ToString();
				}
				else
				{
					sendList[cardCnt / 17] += data.ToString();
				}
				cardCnt++;
				// 17 + 3
			}
			
			Random r = new Random();
			int first = r.Next(0, 3);
			for (int i = 0; i < playersInRoom.Count; i++)
			{
				MsgAllReady msg1 = new MsgAllReady();
				msg1.cardData = sendList[i];
				msg1.num = (first + i) % 3;
				playersInRoom[i].Send(msg1);
			}
		}
	}
	// public static void MsgEnterRoom(ClientState c, MsgBase msgBase){
	// 	MsgEnterRoom msg = (MsgEnterRoom)msgBase;
	// 	Player player = c.player;
	// 	if(player == null) return;
	// 	//已经在房间里
	// 	if(player.roomId >=0 ){
	// 		msg.result = 1;
	// 		player.Send(msg);
	// 		return;
	// 	}
	// 	//获取房间
	// 	Room room = RoomManager.GetRoom(msg.id);
	// 	if(room == null){
	// 		msg.result = 1;
	// 		player.Send(msg);
	// 		return;
	// 	}
	// 	//进入
	// 	if(!room.AddPlayer(player.id)){
	// 		msg.result = 1;
	// 		player.Send(msg);
	// 		return;
	// 	}
	// 	//返回协议	
	// 	msg.result = 0;
	// 	player.Send(msg);
	// }


	//获取房间信息
	public static void MsgGetRoomInfo(ClientState c, MsgBase msgBase){
		MsgGetRoomInfo msg = (MsgGetRoomInfo)msgBase;
		Player player = c.player;
		if(player == null) return;

		Room room = RoomManager.GetRoom(player.roomId);
		if(room == null){
			player.Send(msg);
			return;
		}

		player.Send(room.ToMsg());
	}

	//离开房间
	public static void MsgLeaveRoom(ClientState c, MsgBase msgBase){
		MsgLeaveRoom msg = (MsgLeaveRoom)msgBase;
		Player player = c.player;
		if(player == null) return;

		Room room = RoomManager.GetRoom(player.roomId);
		if(room == null){
			msg.result = 1;
			player.Send(msg);
			return;
		}

		room.RemovePlayer(player.id);
		//返回协议
		msg.result = 0;
		player.Send(msg);
	}


	//请求开始战斗
	public static void MsgStartBattle(ClientState c, MsgBase msgBase){
		MsgStartBattle msg = (MsgStartBattle)msgBase;
		Player player = c.player;
		if(player == null) return;
		//room
		Room room = RoomManager.GetRoom(player.roomId);
		if(room == null){
			msg.result = 1;
			player.Send(msg);
			return;
		}
		//是否是房主
		if(!room.isOwner(player)){
			msg.result = 1;
			player.Send(msg);
			return;
		}
		//开战
		if(!room.StartBattle()){
			msg.result = 1;
			player.Send(msg);
			return;
		}
		//成功
		msg.result = 0;
		player.Send(msg);
	}

}


