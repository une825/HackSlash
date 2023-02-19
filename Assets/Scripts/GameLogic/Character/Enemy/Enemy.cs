using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Stats       m_stat   = null;
    private GameObject  m_Target = null;

    // Start is called before the first frame update
    void Start()
    {
        m_stat = new Stats(100.0f, 3.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (null == m_Target)
            return;

        UpdateMove();
        CheckHP();
    }

    private void LateUpdate()
    {
        UpdateShape();
    }

    public void SetTarget(GameObject goTarget)
    {
        m_Target = goTarget;
    }

    public void SetDamage(float fDamage)
    {
        m_stat.CUR_HP -= fDamage;
    }

    private void UpdateMove()
    {
        float moveValue = m_stat.SPEED * Time.deltaTime;
        var dir = m_Target.transform.localPosition - this.transform.localPosition;
        var followValue = dir *= moveValue;

        transform.localPosition = new Vector3(transform.localPosition.x + followValue.x, transform.localPosition.y + followValue.y, transform.localPosition.z + followValue.z);
    }

    private void CheckHP()
    {
        if(0 >= m_stat.CUR_HP)
        {
            m_stat.CUR_STATUS = Stats.STATUS.DEAD;
        }
    }

    private void UpdateShape()
    {
        switch (m_stat.CUR_STATUS)
        {
            case Stats.STATUS.NONE:
                {

                }
                break;
            case Stats.STATUS.DEAD:
                {
                    var vOriScale = transform.localScale;
                    float fHeight = transform.localScale.y;
                    if (fHeight < 0.0f)
                        break;

                    const float DEAD_ANIM_TIME = 1.0f;
                    float fMoveScaleValue = Mathf.Lerp(fHeight, 0.0f, DEAD_ANIM_TIME);
                    transform.localScale = Vector3.one * fMoveScaleValue;

                    if (fHeight > 0.0f)
                        break;

                    this.gameObject.SetActive(false);
                }
                break;
        }
    }
}
