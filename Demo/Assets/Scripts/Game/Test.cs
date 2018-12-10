using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Enemy
{
    private void Start()
    {
        message.text = "将要造成" + damage + "点伤害";

    }
    public override void  StartAI()
    {
        if (!isWaitPlayer)
        {
            player.OnDamage(damage);
            switch (state)
            {
                case 0:
                    {
                        state++;
                        damage = 5;
                        break;
                    }
                case 1:
                    {
                        state++;
                        damage = 5;
                        break;
                    }
                case 2:
                    {
                        state = 0;
                        damage = 20;
                        break;
                    }
            }
            isWaitPlayer = true;

        }
        message.text = "将要造成" + damage + "点伤害";

    }
}
