using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy0 : Enemy
{
    private void Start()
    {
        message.text = "将要造成" + damage + "点伤害";

    }
    public override void StartAI()
    {
        if (!isWaitPlayer)
        {
            player.OnDamage(damage);
            damage += 2;
            isWaitPlayer = true;
            message.text = "将要造成" + damage + "点伤害";
        }


    }
}
