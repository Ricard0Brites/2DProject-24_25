using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKeyDown && gameObject)
        {
            Destroy(gameObject);
        }
    }
}
