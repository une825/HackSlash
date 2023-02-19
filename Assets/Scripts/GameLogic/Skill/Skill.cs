using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    protected Player m_player = null;

    public Skill()
    {
        InIt();
    }

    public void SetPlayer(Player _player)
    {
        m_player = _player;
    }

    virtual protected void InIt()
    {

    }

    virtual public void PlaySkill()
    {

    }
}
