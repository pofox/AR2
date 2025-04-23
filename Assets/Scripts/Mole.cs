using UnityEngine;

enum MoleState
{
    Idle,
    Up,
    Down,
    Stunned,
    wait
}
public class Mole : MonoBehaviour
{
    [SerializeField] private float StartHight = -0.5f;
    [SerializeField] private float EndHight = 0.5f;
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float WaitTime = 1f;

    [SerializeField] private float StunnTime = 0.5f;
    MoleState m_State = MoleState.Down;
    float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localPosition = new Vector3(0, StartHight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_State)
        {
            case MoleState.Idle:
                break;
            case MoleState.Up:
                upState();
                break;
            case MoleState.Down:
                downState();
                break;
            case MoleState.Stunned:
                stunnedState();
                break;
            case MoleState.wait:
                waitState();
                break;
            default:
                break;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        Stunn();
                        MoleManger.instance.Score++;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    Stunn();
                    MoleManger.instance.Score++;
                }
            }
        }
    }

    public void Stunn()
    {
        m_State = MoleState.Stunned;
    }

    public void Up()
    {
        m_State = MoleState.Up;
    }

    void downState()
    {
        if (transform.localPosition.y > StartHight)
        {
            transform.localPosition = new Vector3(0, Mathf.Lerp(EndHight, StartHight, (transform.localPosition.y - EndHight) / (StartHight - EndHight) + Speed * Time.deltaTime), 0);
        }
        else
        {
            transform.localPosition = new Vector3(0, StartHight, 0);
            m_State = MoleState.Idle;
        }
    }
    void upState()
    {
        if (transform.localPosition.y < EndHight)
        {
            transform.localPosition = new Vector3(0, Mathf.Lerp(StartHight, EndHight, (transform.localPosition.y - StartHight) / (EndHight - StartHight) + Speed * Time.deltaTime), 0);
        }
        else
        {
            transform.localPosition = new Vector3(0, EndHight, 0);
            m_State = MoleState.wait;
        }
    }
    void stunnedState()
    {
        timer += Time.deltaTime;
        if (timer >= StunnTime)
        {
            m_State = MoleState.Down;
            timer = 0f;
        }
    }
    void waitState()
    {
        timer += Time.deltaTime;
        if (timer >= WaitTime)
        {
            m_State = MoleState.Down;
            timer = 0f;
        }
    }
}
