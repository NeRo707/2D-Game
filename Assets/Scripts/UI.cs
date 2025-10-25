using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

  public static UI instance;

  [SerializeField] private GameObject gameOverUI;

  [SerializeField] private TextMeshProUGUI timerText;
  [SerializeField] private TextMeshProUGUI killCountText;

  private int killCount;

  private void Awake() {
    if (instance == null)
      instance = this;
    else
      Destroy(gameObject);
      
    Time.timeScale = 1;
  }

  private void Update() {
    timerText.text = Time.time.ToString("F2") + "s";
  }

  public void ShowGameOverUI() {
    Time.timeScale = .5f;
    gameOverUI.SetActive(true);
  }
  public void RestartLevel() {
    int sceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(sceneIndex);
  }

  public void addKillCount() {
    killCount++;
    killCountText.text = killCount.ToString();
  }
}
