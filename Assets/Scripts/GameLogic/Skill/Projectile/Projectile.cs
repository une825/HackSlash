using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Stats     m_stat = null;
    protected Vector3   m_vDir = Vector3.zero;
    protected float     m_fLifeTime     = 0.0f;
    protected float     m_fCurLifeTime  = 0.0f;

    public void SetDirection(Vector3 _Dir) { m_vDir = _Dir; }
    public void SetLifeTime(float _lifeTime) { m_fLifeTime = _lifeTime; }

    void Start()
    {
        m_stat = new Stats();
        m_stat.SetSpeed(1.5f);
        m_stat.SetMaxHP(1);
        m_stat.SetCurHP(1);
        m_stat.SetAttackPower(100);
    }

    void Update()
    {
        UpdateMove();
        UpdateLifeTime();
    }

    private void LateUpdate()
    {
        UpdateRotation();
        UpdateShape();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var scriptEnemy = collision.gameObject.GetComponent<Enemy>();
            if (null != scriptEnemy)
                scriptEnemy.SetDamage(m_stat.ATTACK_POWER);

            m_stat.CUR_STATUS = Stats.STATUS.DEAD;
        }
    }

    private void UpdateMove()
    {
        float moveValue = m_stat.SPEED * Time.deltaTime;
        var followValue = m_vDir * moveValue;

        transform.localPosition = new Vector3(transform.localPosition.x + followValue.x, transform.localPosition.y + followValue.y, transform.localPosition.z + followValue.z);
    }
    
    private void UpdateRotation()
    {
        var position    = transform.localPosition;
        var target      = m_vDir * 1000;
        var angle       = Mathf.Atan2(target.y - position.y, target.x - position.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void UpdateLifeTime()
    {
        if (m_fCurLifeTime >= m_fLifeTime)
        {
            m_stat.CUR_STATUS = Stats.STATUS.DEAD;
            return;
        }

        m_fCurLifeTime += Time.deltaTime;
    }

    private void UpdateShape()
    {
        switch (m_stat.CUR_STATUS)
        {
            case Stats.STATUS.NONE:
                {
                    var vOriScale = transform.localScale;
                    float fHeight = transform.localScale.y;
                    if (fHeight < 0.0f)
                        break;

                    float fMoveScaleValue = Mathf.Lerp(fHeight, 0.0f, m_fCurLifeTime);
                    transform.localScale = new Vector3(vOriScale.x, fMoveScaleValue, vOriScale.z);
                }
                break;
            case Stats.STATUS.DEAD:
                {
                    this.gameObject.SetActive(false);
                }
                break;
        }
    }

}
