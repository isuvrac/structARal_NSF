using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{

    public void sceneChange(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

}