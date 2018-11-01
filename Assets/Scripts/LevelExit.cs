using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

    [SerializeField] float LevelLoadDelay = 2f;

    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        particle.Play();
        //Invoke("EndLevel", 1f);
        StartCoroutine(LoadlNextLevel());
    }

    IEnumerator LoadlNextLevel()
    {
        FindObjectOfType<ScenePersist>().ResetLevels();
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        EndLevel();
    }

    private void EndLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
