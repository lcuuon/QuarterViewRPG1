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
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            curTime = Time.time;

            slider.value = (curTime - startTime) / 2f;
            if (curTime - startTime > 2)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }


}
