using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public Image progressBar;
    public GameObject lodingScreen;
    public Text text;
    public Transform imageTransform;

    public delegate void HandleOnLoadingScreenProgressUpdated(float progress);
    public event HandleOnLoadingScreenProgressUpdated OnProgressUpdated;

    public delegate void HandleOnLoadingScreenStarted();
    public event HandleOnLoadingScreenStarted OnStarted;

    public delegate void HandleOnLoadingScreenStopped();
    public event HandleOnLoadingScreenStopped OnStopped;

    public void LoadScene(string sceneUid)
    {
        IsLoadingScreenStarted = true;

        Pogress = 0;
        text.text = string.Format(UI_LOADING, sceneUid);

        OnProgressUpdated?.Invoke(0);
        lodingScreen.SetActive(true);
        
        OnStarted?.Invoke();

        StartCoroutine(RotateImage());
        StartCoroutine(LoadSceneAsyncCoroutine(sceneUid));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneUid)
    {
        yield return new WaitForSecondsRealtime(1); // ONLY TO TEST TASK TO HOLD LOADING LONGER
        var loading = SceneManager.LoadSceneAsync(sceneUid);
        loading.allowSceneActivation = false;

        while (loading.progress < preloadProgress)
        {
            UpdateProgress(loading);
            yield return null;
        }

        UpdateProgress(loading);
        yield return new WaitForSecondsRealtime(3); // ONLY TO TEST TASK TO HOLD LOADING LONGER
        IsLoadingScreenStarted = false;
        loading.allowSceneActivation = true;
        lodingScreen.SetActive(false);
        OnStopped?.Invoke();
    }

    private IEnumerator RotateImage()
    {
        while (IsLoadingScreenStarted)
        {
            imageTransform.Rotate(0, 0, -0.5f);
            yield return null;
        }
    }

    private void UpdateProgress(AsyncOperation loading)
    {
        Pogress = loading.progress;
        progressBar.fillAmount = loading.progress;
        OnProgressUpdated?.Invoke(loading.progress);
    }

    public float Pogress { get; private set; }

    public bool IsLoadingScreenStarted { get; private set; }

    private const string UI_LOADING = "LOADING: {0}";
    private const float preloadProgress = 0.9f;

}
