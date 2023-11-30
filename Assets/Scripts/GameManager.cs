using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 respawnOffset = new Vector3(0f, 1f, 0f);

    private Vector3 respawnPoint;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI fallCountText;

    private float timer;
    private int fallCount;

    private bool isGameWon = false;
    public TextMeshProUGUI winText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        respawnPoint = GameObject.Find("Player").transform.position;
    }

    void Update()
    {
        if (!isGameWon)
        {
            timer += Time.deltaTime;
            UpdateUI();
        }
    }

    public void SetCheckpoint(Vector3 position)
    {
        respawnPoint = position + respawnOffset;
        Debug.Log("Checkpoint set at: " + respawnPoint);
    }

    public void PlayerFell()
    {
        fallCount++;
        UpdateUI();
    }

    public Vector3 GetRespawnPoint()
    {
        return respawnPoint;
    }

    void UpdateUI()
    {
        timeText.text = "Time: " + timer.ToString("F2");
        fallCountText.text = "Falls: " + fallCount;
    }

    public void WinGame()
    {
        isGameWon = true;
        Time.timeScale = 0f;
        winText.text = "You Win! GG Yay!";
    }
}
