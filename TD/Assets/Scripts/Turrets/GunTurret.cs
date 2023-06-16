using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTurret : Entity
{

    public override void Die()
    {
        Destroy(gameObject);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
