using TMPro;
using UnityEngine;

public class UI : MonoBehaviour {

  public static UI instance;

  [SerializeField] private TextMeshProUGUI timerText;
  [SerializeField] private TextMeshProUGUI killCountText;

  private int killCount;

  private void Awake() {
    if (instance == null)
      instance = this;
    else
      Destroy(gameObject);
  }

  private void Update() {
    timerText.text = Time.time.ToString("F2") + "s";
  }

  public void addKillCount() {
    killCount++;
    killCountText.text = killCount.ToString();
  }
}
