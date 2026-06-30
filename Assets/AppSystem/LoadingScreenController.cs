using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScreenController : MonoBehaviour
{
    public Image progressBar;
    public GameObject lodingScreen;
    public Text text;
    public Transform imageTransform;

    public event Action<float> OnProgressUpdated;
    public event Action OnStarted;
    public event Action OnStopped;

    private void Awake()
    {
        LoadingDelay1 = new WaitForSecondsRealtime(1);
        LoadingDelay2 = new WaitForSecondsRealtime(3);
    }

    public void LoadScene(string sceneUid)
    {
        text.text = string.Format(UI_LOADING, sceneUid);

        IsLoadingScreenStarted = true;

        Pogress = 0;
        OnProgressUpdated?.Invoke(Pogress);

        lodingScreen.SetActive(true);
        OnStarted?.Invoke();

        StartCoroutine(RotateImage());
        StartCoroutine(LoadSceneAsyncCoroutine(sceneUid));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneUid)
    {
        yield return LoadingDelay1;
        var loading = SceneManager.LoadSceneAsync(sceneUid);
        loading.allowSceneActivation = false;

        while (loading.progress < preloadProgress)
        {
            UpdateProgress(loading);
            yield return null;
        }

        UpdateProgress(loading);
        yield return LoadingDelay2;
        IsLoadingScreenStarted = false;
        loading.allowSceneActivation = true;
        lodingScreen.SetActive(false);
        OnStopped?.Invoke();
    }

    private IEnumerator RotateImage()
    {
        while (IsLoadingScreenStarted)
        {
            imageTransform.Rotate(0, 0, imageRotation);
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

    private const float imageRotation = -0.5f;
    private const float preloadProgress = 0.9f;
    private const string UI_LOADING = "LOADING: {0}";

    private WaitForSecondsRealtime LoadingDelay1 { get; set; }  // ONLY TO TEST TASK TO HOLD LOADING LONGER
    private WaitForSecondsRealtime LoadingDelay2 { get; set; }  // ONLY TO TEST TASK TO HOLD LOADING LONGER

}
