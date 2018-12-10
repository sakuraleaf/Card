using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : Enemy
{
    private void Start()
    {
        message.text = "将要造成" + damage + "点伤害";

    }
    public override void StartAI()
    {
        if (!isWaitPlayer)
        {
            switch (state)
            {
                case 0:
                    {
                        player.OnDamage(damage);
                        break;
                    }
                case 1:
                    {
                        player.OnDamage(damage);
                        OnShield(5);
                        break;
                    }
                case 2:
                    {
                        damage += 3;
                        OnShield(10);
                        break;
                    }
            }
            isWaitPlayer = true;

        }
        state = Random.Range(0, 3);
        switch (state)
        {
            case 0:
                {
                    message.text = "将要造成" + damage + "点伤害";
                    break;
                }
            case 1:
                {
                    message.text = "将要造成" + damage + "点伤害并防御";

                    break;
                }
            case 2:
                {
                    message.text = "准备强化并防御";
                    break;
                }
        }

    }
}
