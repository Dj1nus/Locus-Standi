using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();
    [SerializeField] private int _numberOfWaves;
    [SerializeField] private int[] _numberOfEnemies;
    [SerializeField] private float _delayBetweenWaves;
    [SerializeField] private float _delayBetweenSpawn;

    [SerializeField] private BaseEnemyClass _ram;

    [SerializeField] private float _startDelay;

    private bool _isCanSpawn = false;
    //private List<GameObject> _spawnPoints = new List<GameObject>();
    
    IEnumerator WavesTimer(float time)
    {
        _isCanSpawn = false;
        yield return new WaitForSeconds(time);
        _isCanSpawn = true;   
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }

    void Start()
    {
        StartCoroutine(WavesTimer(_startDelay));
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCanSpawn)
        {
            int counter = 0;
            foreach (GameObject spawnPoint in _spawnPoints)
            {
                for (int i = 0; i < _numberOfEnemies[counter] / _spawnPoints.Count; i++)
                {
                    var newNpc = Instantiate(_ram, new Vector3(spawnPoint.transform.position.x, 0, spawnPoint.transform.position.z), Quaternion.identity);
                    newNpc.Init();
                    StartCoroutine(Timer(_delayBetweenSpawn));
                }

                counter++;
            }
            StartCoroutine(WavesTimer(_delayBetweenWaves));
        }
    }
}
