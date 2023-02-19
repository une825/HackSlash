using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eRecyclerKind
{
    __START__,

    ENEMY,
    PROJECTILE_SLASH,

    __END__,
}

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject m_RecyclerRoot;

    Dictionary<eRecyclerKind, Queue<GameObject>> m_dicPool = new Dictionary<eRecyclerKind, Queue<GameObject>>();

    public string GetPath(eRecyclerKind _eKind)
    {
        string strPath = "Prefabs/";

        switch (_eKind)
        {
            case eRecyclerKind.__START__:
            case eRecyclerKind.__END__:
                break;

            case eRecyclerKind.ENEMY:
                string.Concat(strPath, "Enemy");
                break;
            case eRecyclerKind.PROJECTILE_SLASH:
                string.Concat(strPath, "Projectile_Slash");
                break;
        }


        return strPath;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(eRecyclerKind kind = eRecyclerKind.__START__; kind < eRecyclerKind.__END__; kind++)
        {
            if (m_dicPool.ContainsKey(kind))
                continue;

            Queue<GameObject> queue_GoPool = new Queue<GameObject>();
            m_dicPool.Add(kind, queue_GoPool);

            const int POOL_COUNT = 100;
            for (int i =0; i < POOL_COUNT; ++i)
            {
                GameObject go = Resources.Load(GetPath(kind)) as GameObject;
                if (null == go)
                    continue;

                GameObject go_Clone = MonoBehaviour.Instantiate(go, m_RecyclerRoot.transform);
                if (null == go_Clone)
                    return;

                queue_GoPool.Enqueue(go_Clone);
            }
        }
    }

    public GameObject PopObject(eRecyclerKind _eKind)
    {
        if (false == m_dicPool.ContainsKey(_eKind))
            return null;

        if (m_dicPool[_eKind].Count > 0)
        {
            GameObject go = m_dicPool[_eKind].Dequeue();
            if (null == go)
                return null;

            go.SetActive(true);
            return go;
        }    
        else
        {
            GameObject go = Resources.Load(GetPath(_eKind)) as GameObject;
            if (null == go)
                return null;
            GameObject go_Clone = MonoBehaviour.Instantiate(go, m_RecyclerRoot.transform);
            if (null == go_Clone)
                return null;

            go_Clone.SetActive(true);
            return go_Clone;
        }
    }

    // #TODO_이거 각 오브젝트 class 에서 호출해야합
    public void InputObject(eRecyclerKind _eKind, GameObject _go)
    {
        if (false == m_dicPool.ContainsKey(_eKind))
            return;

        _go.gameObject.SetActive(false);
        _go.transform.parent = m_RecyclerRoot.transform;
        m_dicPool[_eKind].Enqueue(_go);
    }

}