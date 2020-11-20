//查询成绩
public class MsgGetPlayerInfo:MsgBase {
    public MsgGetPlayerInfo() {protoName = "MsgGetPlyaerInfo";}
    //服务端回
    // public int win = 0;
    // public int lost = 0;
    public int bean;
    public int diamond;
    public int cpNum;
}