using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Smoke : MonoBehaviour
{
    void Start()
    {
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(LifeTimer());
    }


    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
