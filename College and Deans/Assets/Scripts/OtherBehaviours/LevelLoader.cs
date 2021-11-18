using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextStage();
        }
#endif
    }

    public void LoadNextStage()
    {
        StartCoroutine(LoadStage("Interlude"));
    }


    IEnumerator LoadStage(string levelName)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait for animation to end
        yield return new WaitForSeconds(transitionTime);

        //Load scene
        FindObjectOfType<GameManager>().NextLevelOrStage();
        SceneManager.LoadScene(levelName);
    }
}
