using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] Slider slider;

    private float startTime = 0;
    private float curTime = 0;

    public IEnumerator LoadAsynScene(string sceneName)
    {
        yield return null;
        startTime = Time.time;
        Debug.Log("loadStart");
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            curTime = Time.time;
            Debug.Log(curTime - startTime);

            slider.value = (curTime - startTime) / 2f;
            if (curTime - startTime > 2)
            {
                Debug.Log("loadEnd");
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }


}
