using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAfterTime : MonoBehaviour
{

    [SerializeField]
    private float delayBeforeLoading = 10f;
    [SerializeField]
    private string sceneNameToLoad;
    private float timeElapsed;

    private void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > delayBeforeLoading)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }

    //Function to skip Introduction text crawl.
    //Assign to a button.
    public void SkipIntroduction()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
