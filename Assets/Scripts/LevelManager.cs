using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float levelReloadDelay = 2;
    [SerializeField] float nextLevelLoadDelay = 2;
    
    private IEnumerator LoadSceneDelayed(float delay, int sceneBuildIndex){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneBuildIndex);
    }
    
    public void ReloadScene(){
        StartCoroutine(LoadSceneDelayed(levelReloadDelay, SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel(){
        int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneBuildIndex > SceneManager.sceneCount - 1){
            nextSceneBuildIndex = 0; // Si estoy en la última escena, loopea a la primera
        }

        StartCoroutine(LoadSceneDelayed(nextLevelLoadDelay, nextSceneBuildIndex));
    }
}
