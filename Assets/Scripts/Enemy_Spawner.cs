using UnityEngine;

public class Enemy_Spawner : MonoBehaviour {
  [SerializeField] private GameObject enemyPrefab;
  [SerializeField] private Transform[] spawnPoints;
  [SerializeField] private float cooldown = 2f;
  [Space]
  [SerializeField] private float cooldownDecreaseRate = .05f;
  [SerializeField] private float cooldownCap = 7f;

  private float timer;
  
  private Transform player;


  private void Start() {
    player = FindFirstObjectByType<Player>().transform;
  }

  private void Update() {
    timer -= Time.deltaTime;
    if (timer <= 0f) {
      timer = cooldown;
      SpawnEnemy();

      cooldown = Mathf.Max(cooldownCap, cooldown - cooldownDecreaseRate);
    }
  }

  private void SpawnEnemy() {
    int spawnIndex = Random.Range(0, spawnPoints.Length);

    Vector3 spawnPosition = spawnPoints[spawnIndex].position;

    GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    bool createdOnRight = newEnemy.transform.position.x > player.position.x;

    if(createdOnRight) {
      newEnemy.GetComponent<Enemy>().Flip();
    }
  }
}
