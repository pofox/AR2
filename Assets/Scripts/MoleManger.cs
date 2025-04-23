using TMPro;
using UnityEngine;

public class MoleManger : MonoBehaviour
{
    [SerializeField] GameObject[] moles;
    [SerializeField] float moletime = 0.5f;
    [SerializeField] TextMeshProUGUI scoreText;
    float timer = 0f;
    int score = 0;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score.ToString();
            }
        }
    }
    public static MoleManger instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > moletime)
        {
            timer = 0f;
            if (Random.value < 0.5f) return;
            int randomIndex = Random.Range(0, moles.Length);
            moles[randomIndex].GetComponent<Mole>().Up();
        }
    }
}
