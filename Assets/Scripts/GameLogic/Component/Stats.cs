using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public enum STATUS
    {
        NONE,
        DEAD,
    }
    private STATUS m_eStatus = STATUS.NONE;
    public STATUS CUR_STATUS { get { return m_eStatus; } set { m_eStatus = value; } }
    public Stats() { }

    public Stats(float fMaxHP, float fSpeed, float fAttackPower)
    {
        m_MaxHP = fMaxHP;
        m_CurHP       = fMaxHP;
        m_Speed       = fSpeed;
        m_AttackPower = fAttackPower;
    }

    private float m_MaxHP = 0;
    public float MAX_HP { get { return m_MaxHP; } }

    private float m_CurHP = 0;
    public float CUR_HP { get { return m_CurHP; } set { m_CurHP = value; } }

    private float m_Speed = 0.0f;
    public float SPEED { get { return m_Speed; } }

    private float m_AttackPower = 0.0f;
    public float ATTACK_POWER { get { return m_AttackPower; } }


    public void SetMaxHP(float _fHP)
    {
        m_MaxHP = _fHP;
    }

    public void SetCurHP(float _fHP)
    {
        m_CurHP = _fHP;
    }

    public void SetSpeed(float _fSpeed)
    {
        m_Speed = _fSpeed;
    }

    public void SetAttackPower(float _fAttackPower)
    {
        m_AttackPower = _fAttackPower;
    }
}
