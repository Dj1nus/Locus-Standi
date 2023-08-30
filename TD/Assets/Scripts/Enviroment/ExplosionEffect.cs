using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<AudioPlayer>().Play(SoundTypes.Explode, Random.Range(0.9f, 1.1f));
    }
}
