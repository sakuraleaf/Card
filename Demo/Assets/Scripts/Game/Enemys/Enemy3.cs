using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy3 : Enemy
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
                        player.isVulnerability += 2;
                        player.StateTable();
                        break;
                    }
                case 2:
                    {
                        damage += 3;
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
                    if(player.isVulnerability > 0)
                        message.text = "将要造成" + (int)(damage * 1.5f) + "点伤害";
                    else
                        message.text = "将要造成" + damage + "点伤害";
                    break;
                }
            case 1:
                {
                    message.text = "对你造成负面效果";

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
