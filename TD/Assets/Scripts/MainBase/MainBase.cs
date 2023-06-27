using System;
using UnityEngine;

public class MainBase : MonoBehaviour
{
    private Entity entity;

    private void Start()
    {
        entity = GetComponent<Entity>();
    }

    private void Update()
    {
        if (entity.GetHp() < 500)
        {
            entity.TakeDamage(-600);
        }
    }
}
