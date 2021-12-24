
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimplePlayerStats 
{
    public string UserId;
    public string userName;
    public int lastcp;
    public int antrelationmeter;
    public bool anthillcompletion;
    public float anthilltiming;
    public long updateon;
    public long createdon;



    public SimplePlayerStats(string UserId, string userName,int lastcp, int antrelationmeter, bool anthillcompletion, float anthilltiming)
    {
        this.UserId = UserId;
        this.userName = userName;
        this.lastcp = lastcp;
        this.antrelationmeter = antrelationmeter;
        this.anthilltiming = anthilltiming;


        var timestamp = this.getTimeUnix();
        this.updateon = timestamp;
        this.createdon = timestamp;
    }

    public long getTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    public string SimplePlayerStatsToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
