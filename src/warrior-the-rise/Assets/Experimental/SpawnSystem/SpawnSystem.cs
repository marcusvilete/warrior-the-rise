using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class SpawnSystem : MonoBehaviour
{
    public float speed = 10f;
    public float TimeBetweenSteps { get { return 1 / speed; } }
    public float currentCountdown;
    private bool isRunning = false;

    private Dictionary<string, ISpawnStep> steps;
    private LevelLoader levelLoader;

    //Spawning
    private Vector3[] spawnPoints;
    public Projectile[] projectilePrefabs;
    public Projectile selectedProjectilePrefab;
    public Transform playableArea;


    public event Action SpeedChanged;
    public event Action ProjectileChanged;
    public event Action LevelLoaded;
    public event Action LevelFinished;

    //??
    public event Action StepStarted;
    public event Action StepFinished;



    private void Awake()
    {
        SetupSteps();
        SetupSpawners();
        levelLoader = new LevelLoader();
        currentCountdown = TimeBetweenSteps;
        selectedProjectilePrefab = projectilePrefabs[0];

        //xxx
        LoadLevel("Level1");
    }

    private void SetupSteps()
    {
        steps = new Dictionary<string, ISpawnStep>();

        steps.Add("SetSpeed", new SetSpeed(this));
        steps.Add("SetProjectile", new SetProjectile(this));
        steps.Add("Spawn", new Spawn(this));
        steps.Add("Pause", new Pause(this));
    }

    public void LoadLevel(string level)
    {
        levelLoader.ReadContent(level);
        LevelLoaded?.Invoke();
        //xxx
        StartLevel();
    }

    public void StartLevel()
    {
        isRunning = true;
    }

    private void RunStep()
    {
        string step = null;
        string args = null;

        if (levelLoader.GetNextStep(out step, out args))
        {
            //If there is a next step, run
            steps[step].Run(args);
        }
        else
        {
            //If there is not a next step, finish level
            isRunning = false;
            LevelFinished?.Invoke();
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            if (currentCountdown <= 0f)
            {
                currentCountdown = TimeBetweenSteps;
                RunStep();
            }
            currentCountdown -= Time.deltaTime;
        }
    }


    //Spawning
    private void SetupSpawners()
    {

        Vector2 lowerBound = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 upperBound = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float units = (upperBound.x - lowerBound.x) / 8;
        //lowerBound.x += units * 0.06f;

        spawnPoints = new Vector3[8];
        for (int i = 0; i < 8; i++)
        {
            spawnPoints[i] = new Vector3(lowerBound.x + (i * units) + (units / 2), -(2 + Camera.main.orthographicSize), 0);
            //Debug.Log($"[x:{spawnPoints[i].x}], [y:{spawnPoints[i].y}]");
        }
    }

    public void Spawn(Projectile prefab, int spawnPointIndex)
    {
        if (spawnPointIndex < spawnPoints.Length)
        {
            //TODO: Pool?
            var proj = Instantiate(prefab, playableArea);
            proj.transform.position = spawnPoints[spawnPointIndex];
            proj.SetProjectileSpeed(speed);
        }
    }
}

