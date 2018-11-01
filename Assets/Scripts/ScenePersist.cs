using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {

    //private int startingSceneIndex;

    private void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        //startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //if(currentSceneIndex == startingSceneIndex)
        //{
        //    Destroy(gameObject);
        //}
    }

    public void ResetLevels()
    {
        Destroy(gameObject);
    }
}
