using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Explosion : MonoBehaviour
{
    [SerializeField] private VisualEffect _explosion;

    public void Explode(float radius, float damage)
    {
        if (transform.position.y < 0)
        {
            transform.position += new Vector3(0, 1f, 0);
        }


        Instantiate(_explosion, transform);


        GetComponentInChildren<MeshRenderer>().enabled = false;

        _explosion.Play();

        Collider[] overlapedColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in overlapedColliders)
        {
            if (col.TryGetComponent(out EnemyEntity enemy))
            {
                enemy.TakeDamage(damage);
            }
        }

        StartCoroutine(WaitUntilEnd());
    }

    private IEnumerator WaitUntilEnd()
    {
        //yield return new WaitForSeconds(_explosion.main.duration - 1f);
        yield return new WaitForSeconds(GetComponentInChildren<AudioPlayer>().GetSoundDuration("Explode"));
        Destroy(gameObject);
    }
}
