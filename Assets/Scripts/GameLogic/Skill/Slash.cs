using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : Skill
{
    protected float m_fOffset = 0.0f;

    public Slash() { }


    override protected void InIt()
    {
        base.InIt();

        m_fOffset = 2.5f;
    }

    override public void PlaySkill()
    {
        GameObject go_projectile = Resources.Load("Prefabs/Projectile_Slash") as GameObject;
        if (null == go_projectile)
            return;

        GameObject go_Clone = MonoBehaviour.Instantiate(go_projectile);
        if (null == go_Clone)
            return;

        if (null == m_player)
            return;

        var dir = m_player.GetDirection().normalized;
        go_Clone.transform.localPosition = (m_player.transform.localPosition) + (dir * m_fOffset);
        
        var compProjectile = go_Clone.GetComponent<Projectile>();
        if (null == compProjectile)
            return;

        compProjectile.SetDirection(dir);
        compProjectile.SetLifeTime(0.3f);
    }
}
