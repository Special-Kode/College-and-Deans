using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene("Interlude"));
    }

    IEnumerator LoadScene(string levelName)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait for animation to end
        yield return new WaitForSeconds(transitionTime);

        //Load scene
        SceneManager.LoadScene(levelName);
    }
}
