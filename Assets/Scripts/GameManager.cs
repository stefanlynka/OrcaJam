using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public String nextLevelName;
    public SpriteRenderer levelRenderer;

    public GameObject Player;

    public ProgressBar ProgressBar;
    public float AttachedCells = 0.0f;
    public float TargetAttachedCells = 5.0f;

    public GameObject DeathScreen;
    public Camera mainCamera;
    public bool isDead;

    public float ProjectileDistanceLimit = 0;

    private bool isZoomingOutForLevelCompletion = false;

    public void Start()
    {
        ProgressBar.UpdateProgress(0);

    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Update()
    {
        if (isDead && Input.GetKey(KeyCode.Space)) SetIsDeadState(false);
    }

    public void ChangeAttachedCells(float amount) {
        AttachedCells += amount;
        AttachedCells = Mathf.Clamp(AttachedCells, 0, TargetAttachedCells);
        ProgressBar.UpdateProgress(AttachedCells / TargetAttachedCells);
        mainCamera.GetComponent<CameraFollow>().ChangeZoom(amount / 5);
        if (AttachedCells >= TargetAttachedCells) {
            OnAttachedTargetReached();
        }
    }

    public void SetIsDeadState(bool isDead) {
        this.isDead = isDead;
        if (isDead) DeathScreen.SetActive(true);
        else {
            DeathScreen.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        }
    }

    public void OnAttachedTargetReached() {
        Player.GetComponent<PlayerInput>()?.SetInputEnabled(false);
        mainCamera.GetComponent<CameraFollow>().ZoomOutOnLevelCompletion(LoadNextLevel);
        SwooshAudio.Instance.PlaySwoosh();
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(nextLevelName);
    }

    public bool CheckIfZoomingOutForLevelCompletion() {
        return isZoomingOutForLevelCompletion;
    }

    public SpriteRenderer GetLevelRenderer() {
        return levelRenderer;
    }
}
