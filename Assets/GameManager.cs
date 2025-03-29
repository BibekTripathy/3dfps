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
        public List<Target> targets = new List<Target>(); // Added initialization
        [HideInInspector] public int activeTargets;
    }

    [Header("Target Walls")]
    public List<TargetWall> walls = new List<TargetWall>();

    [Header("Game Settings")]
    public Material hitMaterial;
    public TextMeshProUGUI scoreText;
    public int currentScore = 0; // Initialize
    public float restartDelay = 2f;

    private int totalActiveTargets;

    void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializeTargets();
    }

    void InitializeTargets()
    {
        totalActiveTargets = 0;
        
        // Clear null references first
        foreach (TargetWall wall in walls)
        {
            wall.targets.RemoveAll(item => item == null);
        }

        foreach (TargetWall wall in walls)
        {
            wall.activeTargets = wall.targets.Count;
            totalActiveTargets += wall.activeTargets;

            foreach (Target target in wall.targets)
            {
                if (target != null)
                {
                    target.gameObject.SetActive(true);
                    target.wasHit = false;
                }
            }
        }
    }

    public void RegisterHit(Target hitTarget, bool wasCorrectHit)
    {
        if (hitTarget == null) return; // Added null check

        if (wasCorrectHit)
        {
            currentScore += 10;
            if (scoreText != null) // Null check for UI
            {
                scoreText.text = "Score: " + currentScore;
            }

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
                StartCoroutine(RestartGame());
            }
        }
        else
        {
            GameOver();
        }
    }

    IEnumerator RestartGame()
    {
        Debug.Log("All targets cleared! Restarting...");
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        Debug.Log("Game Over! Wrong target hit.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}