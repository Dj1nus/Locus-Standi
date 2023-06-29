using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform _muzzle;

    [SerializeField] float _shootForce;

    public void Shoot(Entity target)
    {
        gameObject.transform.LookAt(target.transform.position);

        //var newBullet = Instantiate(bullet, _muzzle.position, Quaternion.identity);
        var newBullet = Instantiate(bullet);

        newBullet.transform.position = _muzzle.position;

        Vector3 direction = target.transform.position - _muzzle.position;

        //newBullet.transform.LookAt(target.transform.position);
        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootForce, ForceMode.Impulse);

        //target.TakeDamage(10000f);
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
