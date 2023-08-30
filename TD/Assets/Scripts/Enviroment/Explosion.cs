using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public void Explode()
    {
        if (transform.position.y < 0)
        {
            transform.position += new Vector3(0, 1f, 0);
        }

        GetComponent<ParticleSystem>().Play();
        GetComponentInChildren<AudioPlayer>().Play(SoundTypes.Explode, Random.Range(0.9f, 1.1f));

        StartCoroutine(WaitUntilEnd());
    }

    private IEnumerator WaitUntilEnd()
    {
        yield return new WaitForSeconds(GetComponentInChildren<AudioPlayer>().GetSoundDuration(SoundTypes.Explode));

        Destroy(gameObject);
    }
}
