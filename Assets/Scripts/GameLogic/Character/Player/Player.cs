using UnityEngine;

public class Player : MonoBehaviour
{
    private Stats m_stat = null;
    private Skill m_skill = null;
    private Vector3 m_Dir = Vector3.zero;

    public void SetSkill(Skill _skill)
    {
        m_skill = _skill;
    }

    public Vector3 GetDirection() { return m_Dir; }

    private void Awake()
    {
        m_stat = new Stats();
        m_stat.SetSpeed(10);
        m_stat.SetMaxHP(100);
        m_stat.SetCurHP(100);
        m_stat.SetAttackPower(20);

        m_skill = new Slash();
        m_skill.SetPlayer(this);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var scriptEnemy = collision.gameObject.GetComponent<Enemy>();
            if (null != scriptEnemy)
                scriptEnemy.SetDamage(m_stat.ATTACK_POWER);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        UpdateRotation();
        UpdateAttack();
    }

    void UpdateMove()
    {
        float moveValue = m_stat.SPEED * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition = new Vector3(transform.localPosition.x - moveValue, transform.localPosition.y, transform.localPosition.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition = new Vector3(transform.localPosition.x + moveValue, transform.localPosition.y, transform.localPosition.z);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + moveValue, transform.localPosition.z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - moveValue, transform.localPosition.z);
        }
    }

    void UpdateRotation()
    {
        var mousePos    = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var position    = this.transform.localPosition;
        m_Dir           = mousePos - position;

        var angle = Mathf.Atan2(mousePos.y - position.y, mousePos.x - position.x) * Mathf.Rad2Deg;
        this.transform.localRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void UpdateAttack()
    {
        if (null == m_skill)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_skill.PlaySkill();
        }
    }
}
