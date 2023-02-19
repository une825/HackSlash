using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject m_EnemyRoot;
    public GameObject m_PlayerParent;
    public GameObject m_Enemy;

    public float  m_SpawnDelay      = 0.5f;
    private float m_SpawnDelayCheck = 0.0f;

    private GameObject m_Player = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (null == m_Player)
        {
            if (null == m_PlayerParent)
                return;
            var trPlayer = m_PlayerParent.transform.GetChild(0);
            if (null == trPlayer)
                return;

            m_Player = trPlayer.gameObject;
            if (null == m_Player)
                return;
        }

        m_SpawnDelayCheck += Time.deltaTime;

        if (m_SpawnDelayCheck >= m_SpawnDelay)
        {
            m_SpawnDelayCheck = 0.0f;
            var goEnemy = Instantiate(m_Enemy, Vector3.zero, Quaternion.identity, m_EnemyRoot.transform);
            if (null == goEnemy)
                return;
            var enemyScript = goEnemy.GetComponent<Enemy>();
            if (null != enemyScript)
                enemyScript.SetTarget(m_Player);
        }
    }
}
