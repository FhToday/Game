using System.Collections;
using System.Collections.Generic;

public class cardData
{
    public int value;
    public int color;
    // Start is called before the first frame update
    public cardData(int value,int color)
    {
        this.value = value;
        this.color = color;
    }

    public override string ToString()
    {
        return value.ToString() + "," + color.ToString() + ",";
    }
}

