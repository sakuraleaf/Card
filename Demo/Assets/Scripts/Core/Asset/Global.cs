using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global {

    private static Global instance;
    public static Global GetInstance()
    {
        if (instance == null)
        {
            instance = new Global();
        }
        return instance;
    }
    public string loadName = "";


    public Hashtable cardID = new Hashtable();

}
