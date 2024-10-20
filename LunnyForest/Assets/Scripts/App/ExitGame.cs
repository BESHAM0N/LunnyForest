using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public sealed class ExitGame 
{
    public void Finish()
    {
        #if UNITY_EDITOR    
        EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
        #endif

    }
}
