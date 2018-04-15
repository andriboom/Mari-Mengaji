using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public bool isMainMenu;
    public Animator animator;
    public string previousScene;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isMainMenu) {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
                Application.OpenURL(webplayerQuitURL);
#else
                Application.Quit();
#endif
            } else
            {
                LoadSceneByName(previousScene);
            }
        }
    }

    public void LoadSceneByName(string sceneName) {
        if (sceneName != string.Empty)
            SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneAsyncAnim(int i) {
        StartCoroutine(ASyningScene(i));
    }

    private IEnumerator ASyningScene (int i) {
        animator.SetTrigger("FadeOut");
        AsyncOperation async = SceneManager.LoadSceneAsync(i);
        // while (!async.isDone)
        //     yield return null;
        // async.allowSceneActivation = false;    
        yield return new WaitForSeconds (2f);
        
        async.allowSceneActivation = true;
        
    }

}
