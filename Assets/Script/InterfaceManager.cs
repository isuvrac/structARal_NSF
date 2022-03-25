using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{

    [SerializeField]
    Button skywalk, Campanile, TownBuilding, Catthall;

    // Start is called before the first frame update
    void Start()
    {
        skywalk.onClick.AddListener(skywalkonlick);
    }

    void skywalkonlick() {
        SceneManager.LoadScene("Skywalk_Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
