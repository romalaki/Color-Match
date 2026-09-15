using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ball;
 
    [Header("Spawn")]
    public float spawnInterval = 0.9f;
    public float minSpawnInterval = 0.35f;
    public float intervalDecreasePerSecond = 0.01f;
    public float minX = -4f;
    public float maxX = 4f;
    public float edgePadding = 0.5f;
 
    private float spawnTimer;
    private Camera cam;
    private bool isSpawning = true;
 
    private void Start()
    {
        Time.timeScale = 1;
        cam = Camera.main;
    }
 
    private void Update()
    {
        if (!isSpawning) return;
 
        // постепенное усложнение
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - intervalDecreasePerSecond * Time.deltaTime);
 
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnShape();
        }
    }
    
    public void SpawnShape()
    {
        float spawnX = GetRandomSpawnX();
        float spawnY = GetSpawnY();
 
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        Instantiate(ball, spawnPos, Quaternion.identity, transform); 
    }
 
    private float GetRandomSpawnX()
    {
        float min, max;
        float dist = Mathf.Abs(cam.transform.position.z - transform.position.z);
        Vector3 leftEdge = cam.ScreenToWorldPoint(new Vector3(0, 0, dist));
        Vector3 rightEdge = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, dist));
        min = leftEdge.x + edgePadding;
        max = rightEdge.x - edgePadding;
        return Random.Range(min, max);
    }
 
    private float GetSpawnY()
    {
        float dist = Mathf.Abs(cam.transform.position.z - transform.position.z);
        Vector3 topEdge = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, dist));
        return topEdge.y + 0.5f;
    }
    
    public void SetSpawning(bool active)
    {
        isSpawning = active;
    }
}
