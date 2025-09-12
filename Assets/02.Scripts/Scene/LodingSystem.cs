using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LodingSystem : MonoBehaviour
{
    static string nextScene;

    public static void LoadScene(string SceneName)
    {
        nextScene = SceneName;
        SceneManager.LoadScene("Loding");
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op =  SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        Task allDataLoadTask = GameManager.Instance.LoaderContainer.LoadAllDataLoaders();

        float timer = 0f;
        float minmumTime = 2f;

        while (op.progress<0.9f || !allDataLoadTask.IsCompleted|| timer < minmumTime)
        {
            timer += Time.unscaledDeltaTime;
            //로딩바 로직 가능
            yield return null;
        }


        if (allDataLoadTask.IsFaulted)
            yield break;

        op.allowSceneActivation = true;
    }
}
