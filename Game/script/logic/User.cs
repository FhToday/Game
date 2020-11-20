namespace Game.script.logic
{
    using System;

    public class User
    {
        //id
        public string id = "";

        //指向ClientState
        public ClientState state;

        //构造函数
        public User(ClientState state)
        {
            this.state = state;
        }
        //数据库数据
        public PlayerData data;

        //发送信息
        public void Send(MsgBase msgBase)
        {
            NetManager.Send(state, msgBase);
        }

    }


}