using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp();
        print(GetHp());
    }
}
