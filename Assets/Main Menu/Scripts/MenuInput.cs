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
