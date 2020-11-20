using System;


public partial class MsgHandler {

	//查询玩家信息
	public static void MsgGetPlayerInfo(ClientState c, MsgBase msgBase){
		MsgGetPlayerInfo msg = (MsgGetPlayerInfo)msgBase;
		Player player = c.player;
		PlayerData playerData = c.player.data;
		if(player == null) return;

		msg.bean = playerData.bean;
		msg.diamond = playerData.diamond;
		msg.cpNum = playerData.cpNum;

		player.Send(msg);
	}

}


