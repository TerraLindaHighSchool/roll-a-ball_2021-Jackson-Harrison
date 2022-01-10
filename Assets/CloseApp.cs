using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : MonoBehaviour
{

    private void FixedUpdate()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

    }
}
