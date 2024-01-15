using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneReloadDelay = 2;
    public void ReloadScene(){
        StartCoroutine(LoadSceneDelayed(sceneReloadDelay, SceneManager.GetActiveScene().buildIndex));
    }

    private IEnumerator LoadSceneDelayed(float delay, int sceneBuildIndex){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
