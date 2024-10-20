using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public ProgressBar ProgressBar;
    public float AttachedCells = 0.0f;
    public float TargetAttachedCells = 25.0f;

    public float ProjectileDistanceLimit = 0;

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

    public void Update() {
        print(AttachedCells);
    }

    public void ChangeAttachedCells(float amount) {
        AttachedCells += amount;
        AttachedCells = Mathf.Clamp(AttachedCells, 0, TargetAttachedCells);
        ProgressBar.UpdateProgress(AttachedCells / TargetAttachedCells);
    }
}
