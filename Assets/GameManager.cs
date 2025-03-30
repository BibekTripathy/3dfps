using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [System.Serializable]
    public class TargetWall
    {
        public string wallName;
        public List<Target> targets = new List<Target>();
        [HideInInspector] public int activeTargets;
        [HideInInspector] public List<Color> originalColors = new List<Color>();
        public Transform wallTransform; // Reference to the wall's transform
    }

    [Header("Target Walls")]
    public List<TargetWall> walls = new List<TargetWall>();

    [Header("Game Settings")]
    public Material hitMaterial;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;
    public int currentScore = 0;
    public int currentLevel = 1;
    public float restartDelay = 2f;
    public float roundTime = 45f; // 45 seconds per round

    private int totalActiveTargets;
    private bool gameOver = false;
    private float currentTime;
    private bool timerRunning = false;

    void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializeGame();
    }

    void InitializeGame()
    {
        currentScore = 0;
        currentLevel = 1;
        UpdateUI();
        ResetTimer();
        InitializeTargets();
        StoreOriginalColors();
    }

    void ResetTimer()
    {
        currentTime = roundTime;
        timerRunning = true;
    }

    void Update()
    {
        if (timerRunning && !gameOver)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();

            if (currentTime <= 0)
            {
                currentTime = 0;
                timerRunning = false;
                GameOver("Time's Up!");
            }
        }
    }

    void InitializeTargets()
{
    totalActiveTargets = 0;
    
    foreach (TargetWall wall in walls)
    {
        wall.targets.RemoveAll(item => item == null);
        wall.activeTargets = wall.targets.Count;
        totalActiveTargets += wall.activeTargets;

        for (int i = 0; i < wall.targets.Count; i++)
        {
            Target target = wall.targets[i];
            if (target != null)
            {
                // Restore original color
                if (i < wall.originalColors.Count)
                {
                    Renderer renderer = target.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        // Ensure we're working with an instance material
                        if (!renderer.material.name.EndsWith("(Instance)"))
                        {
                            renderer.material = new Material(renderer.material);
                        }
                        renderer.material.color = wall.originalColors[i];
                    }
                }
                
                target.gameObject.SetActive(true);
                target.wasHit = false;
                target.ResetTarget();

                // Increase difficulty
                if (target.TryGetComponent<MovingTarget>(out var movingTarget))
                {
                    movingTarget.speed *= (1 + (currentLevel - 1) * 0.2f);
                }
            }
        }
    }
}

    void StoreOriginalColors()
{
    foreach (TargetWall wall in walls)
    {
        wall.originalColors.Clear(); // Clear existing colors
        
        foreach (Target target in wall.targets)
        {
            if (target != null)
            {
                // Store the material's color (creates instance if needed)
                Renderer renderer = target.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Ensure we're working with an instance material
                    if (!renderer.material.name.EndsWith("(Instance)"))
                    {
                        renderer.material = new Material(renderer.material);
                    }
                    wall.originalColors.Add(renderer.material.color);
                }
            }
        }
    }
}
    public void RegisterHit(Target hitTarget, bool wasCorrectHit)
    {
        if (hitTarget == null || gameOver) return;

        if (wasCorrectHit)
        {
            currentScore += 10 * currentLevel; // Score multiplier based on level
            UpdateUI();

            bool foundTarget = false;
            foreach (TargetWall wall in walls)
            {
                if (wall.targets.Contains(hitTarget))
                {
                    wall.activeTargets--;
                    totalActiveTargets--;
                    foundTarget = true;
                    break;
                }
            }

            if (!foundTarget)
            {
                Debug.LogWarning("Hit target not found in any wall!");
                return;
            }

            if (totalActiveTargets <= 0)
            {
                currentLevel++;
                StartCoroutine(RestartRound());
            }
        }
        else
        {
            GameOver("Wrong Target Hit!");
        }
    }

    IEnumerator RestartRound()
    {
        timerRunning = false;
        Debug.Log($"Level {currentLevel - 1} cleared! Starting Level {currentLevel}");
        yield return new WaitForSeconds(restartDelay);
        
        ResetTimer();
        InitializeTargets(); // Boards respawn in original positions
    }

    void GameOver(string reason)
    {
        gameOver = true;
        Debug.Log($"Game Over! {reason} Reached Level {currentLevel}. Final Score: {currentScore}");
        StartCoroutine(ResetGameAfterDelay());
    }

    IEnumerator ResetGameAfterDelay()
    {
        yield return new WaitForSeconds(restartDelay);
        gameOver = false;
        InitializeGame();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = $"Score: {currentScore}";
        if (levelText != null) levelText.text = $"Level: {currentLevel}";
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {Mathf.CeilToInt(currentTime)}s";
            timerText.color = currentTime < 10f ? Color.red : Color.white;
        }
    }
 
}