using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
	private static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

	private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
	#endregion

    public delegate void OnNewWaveEnter();
    public OnNewWaveEnter onNewWaveEnter;

    public delegate void OnScoreChange();
    public OnScoreChange onScoreChange;

    public delegate void OnPlayerDeath();
    public OnPlayerDeath onPlayerDeath;

    public delegate void OnWaveEliminated();
    public OnWaveEliminated onWaveEliminated;

    public int gameScore = 0;
    [Space]
    public List<Enemy> enemies = new List<Enemy>();
    [Space]
    public int wave = 0;
    public float waveDelay = 1.5f;
    public Vector3 troopsPosition = new Vector3(0,-2.15f,0);
    public List<GameObject> waveTroops = new List<GameObject>();
    [Space]
    [Range(0, 100)] public float BuffChance;
    public Collectibles buff;

    private bool isNewWave = true;

    // Start is called before the first frame update
    void Start()
    {
        StartNewWave();
        FloodEnemy();
    }

    private void FloodEnemy()
    {
        enemies.AddRange(FindObjectsOfType<Enemy>());
        foreach (Enemy enemy in enemies)
        {
            enemy.onEnemyDied += RemoveEnemy;
        }

        if(enemies.Count > 0){
            isNewWave = false;
        } 

        if(onNewWaveEnter != null) onNewWaveEnter.Invoke();

    }

    private void RemoveEnemy(Enemy enemy){
        enemies.Remove(enemy);
    }

    public void AddScore(int score){
        gameScore += score;
        if(onScoreChange != null) onScoreChange.Invoke();
    }

    public void GameOver(){
        if(onPlayerDeath != null) onPlayerDeath.Invoke();
    }

    public void Victory(){
        if(onWaveEliminated != null) onWaveEliminated.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isNewWave) CheckTroopsDefeated();

    }

    private void CheckTroopsDefeated()
    {
        if(enemies.Count == 0){
            if(wave < waveTroops.Count){
                StartNewWave();
                isNewWave = true;
            }else{
                Victory();
            }
        }
    }

    private void StartNewWave(){
        StartCoroutine(EnterNewWave());
        wave++;
    }

    IEnumerator EnterNewWave(){
        yield return new WaitForSeconds(1.5f);
        GameObject newWave = Instantiate(waveTroops[wave-1], new Vector3(0, 6, 0), Quaternion.identity);
        FloodEnemy();

        float timeWait = 0;
        while (timeWait <= waveDelay)
        {
            newWave.transform.position = Vector3.Lerp(newWave.transform.position, troopsPosition, (timeWait / waveDelay));
            timeWait += Time.deltaTime;
            yield return null;
        }
        newWave.transform.position = troopsPosition;
        yield return new WaitForSeconds(0.5f);
    }
}
