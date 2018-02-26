using System;

[Serializable]
public class DBProfile_Login
{
    public int id;
    public string name;

    public DBProfile_Login(int id,string name)
    {
        this.id = id;
        this.name = name;
    }
}
