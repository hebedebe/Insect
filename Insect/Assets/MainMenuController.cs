using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void switchScene(string scene)
    {
        Application.LoadLevel(scene);
    }
}
