using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnersObject
    {
        public GameObject prefab;
    }

    public SpawnersObject[] objects;
    public float spawnRate = 20f; // Tần suất sinh pipe
    public float currentSpawnRate;
    public List<GameObject> spawnedPipes = new List<GameObject>();
    private GameManager gameManager;
    public float gameSpeed;
    private float lastSpawnTime; // Thời điểm sinh lần cuối

    private void Start()
    {
        // Lấy tham chiếu tới GameManager
        gameManager = FindObjectOfType<GameManager>();
        currentSpawnRate = 2f;
        lastSpawnTime = Time.time;  // Ghi lại thời điểm bắt đầu sinh
        ScheduleNextSpawn();  // Lên lịch lần sinh đầu tiên
    }

    private void Update()
    {
        if (gameManager != null)
        {
            // Cập nhật currentSpawnRate dựa trên gameSpeed
            gameSpeed = gameManager.gameSpeed;
            currentSpawnRate = Mathf.Max(spawnRate / gameSpeed, 1.2f);
            // Debug.Log("GameSpeed: " + gameSpeed + ", CurrentSpawnRate: " + currentSpawnRate);

            // Kiểm tra nếu thời gian trống đã vượt quá mức cho phép thì sinh pipe ngay
            if (Time.time - lastSpawnTime >= currentSpawnRate)
            {
                CancelInvoke(nameof(Spawn));
                Spawn();  // Sinh pipe ngay lập tức 
            }
        }
    }

    private void Spawn()
    {
        // Sinh ra một pipe mới
        int randomIndex = Random.Range(0, objects.Length);
        GameObject newPipe = Instantiate(objects[randomIndex].prefab, transform.position, Quaternion.identity);
        spawnedPipes.Add(newPipe.gameObject);

        // Ghi lại thời gian sinh pipe
        lastSpawnTime = Time.time;

        // Lên lịch lần sinh tiếp theo
        ScheduleNextSpawn();
    }

    private void ScheduleNextSpawn()
    {
        // Đảm bảo pipe sẽ được sinh sau khoảng thời gian currentSpawnRate
        Invoke(nameof(Spawn), currentSpawnRate);
    }
}
