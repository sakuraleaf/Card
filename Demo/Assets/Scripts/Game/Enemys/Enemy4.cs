using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy4 : Enemy
{
    private void Start()
    {
        message.text = "将要造成 5X " + damage + "点伤害";

    }
    public override void StartAI()
    {
        if (!isWaitPlayer)
        {
            switch (state)
            {
                case 0:
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            player.OnDamage(damage);
                        }
                        break;
                    }
                case 1:
                    {
                        player.OnDamage(damage * 6);
                        break;
                    }
                case 2:
                    {
                        damage += 1;
                        OnShield(5);
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
                    message.text = "将要造成 5X " + damage + "点伤害";
                    break;
                }
            case 1:
                {
                    message.text = "将要造成" + 6 * damage + "点伤害";
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
