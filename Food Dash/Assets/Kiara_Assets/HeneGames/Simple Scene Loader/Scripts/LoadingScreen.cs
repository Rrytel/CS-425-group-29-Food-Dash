using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace HeneGames.Sceneloader
{
    public class LoadingScreen : MonoBehaviour
    {
        private float progress;

        [Header("Testing graf")]
        [SerializeField] private bool fakeLoading;

        [Header("References")]
        [SerializeField] private GameObject loadingContent;

        [SerializeField] private Transform movingLoadingThing;

        [SerializeField] private Image loadingFillImage;

        [SerializeField] private Text loadingText;

        [Header("Loading knob positions")]
        [SerializeField] private Transform startPoint;

        [SerializeField] private Transform endPoint;

        private void Awake()
        {
            if (fakeLoading)
            {
                loadingContent.SetActive(true);
            }
            else
            {
                loadingContent.SetActive(false);
            }
        }

        private void Update()
        {
            if (fakeLoading)
            {
                if (progress < 1f)
                {
                    progress += Time.deltaTime * 0.1f;
                    float _prosentProgress = progress * 100f;
                    loadingText.text = _prosentProgress.ToString("F0") + "%";

                    loadingFillImage.fillAmount = progress;
                    movingLoadingThing.localPosition = new Vector3(Mathf.Lerp(startPoint.localPosition.x, endPoint.localPosition.x, progress), 0f, 0f);
                }
            }
        }

        public void LoadScene(int _sceneIndex)
        {
            StartCoroutine(LoadAsynchronously(_sceneIndex));
        }

        IEnumerator LoadAsynchronously(int _sceneIndex)
        {
            AsyncOperation _opearation = SceneManager.LoadSceneAsync(_sceneIndex);

            loadingContent.SetActive(true);

            while (!_opearation.isDone)
            {
                float _progress = Mathf.Clamp01(_opearation.progress / 0.9f);
                float _prosentProgress = _progress * 100f;
                loadingFillImage.fillAmount = _progress;
                loadingText.text = _prosentProgress.ToString("F0") + "%";
                movingLoadingThing.localPosition = new Vector3(Mathf.Lerp(startPoint.localPosition.x, endPoint.localPosition.x, _progress), 0f, 0f);

                progress = _prosentProgress;

                yield return null;
            }
        }

        public float Progress()
        {
            return progress;
        }
    }
}