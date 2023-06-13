using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void OnEnable()
    {
        Entity.OnBaseDestroyed += StopGame;
    }

    private void OnDisable()
    {
        Entity.OnBaseDestroyed -= StopGame;
    }

    private void StopGame()
    {
        Time.timeScale = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
