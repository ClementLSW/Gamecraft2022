using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCustomCommands : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Zawarudo.StartZaWarudo();
        }
        if (Input.GetKeyDown(KeyCode.X))
            Zawarudo.EndZaWarudo();

#endif
    }
}
