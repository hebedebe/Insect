using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        //Screen.SetResolution(960, 544, true);
    }
}
