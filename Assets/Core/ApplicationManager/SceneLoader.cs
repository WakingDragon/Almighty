using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BP.Core
{
    [System.Serializable]
    public class SceneListItem
    {
        public string sceneName;    //must match the scene names in editor
        public int sceneIndex;
        public Scene scene;
        public bool isLoaded = false;
        public bool isActive = false;   //this is the scene to whcih new GOs are added
        public bool isGameplayScene = false;    //controls if pause/esc works

        public SceneListItem(string newSceneName)
        {
            sceneName = newSceneName;
        }
    }

    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private float introLoadDelayTime = 2f;

        [Header("Scenes")]
        /// <summary> PRE-REQUISITES
        /// This package requires the following scenes to exist:
        /// The CORE scene, INTRO credits scene, MENU scene, sandbox scene, game scene
        /// Scenes must be added to build settings
        /// </summary>

        [Header("Fixed Scenes: Use name strings!")]
        [SerializeField] private SceneListItem coreScene = new SceneListItem("CoreScene");
        [SerializeField] private SceneListItem introScene = new SceneListItem("IntroCreditScene");
        [SerializeField] private SceneListItem menuScene = new SceneListItem("MenuScene");
        [SerializeField] private SceneListItem sandboxScene = new SceneListItem("SandboxScene");
        [SerializeField] private SceneListItem gameScene = new SceneListItem("GameScene");

        [Header("Master Scenes Library")]
        [SerializeField] private List<SceneListItem> sceneLibrary = new List<SceneListItem>();

        private SceneListItem lastActiveScene;

        AudioListenerManager listenerManager;
        Pause pause;

        #region singleton and awake
        public static SceneLoader instance;

        private void Awake()
        {
            instance = this;

            PopulateSceneLibrary();

            GetSceneAndIndexOfScene(coreScene);  //only available when loaded
            coreScene.isActive = true;
            SetSceneLoaded(coreScene, true);
            SetSceneGameplay(coreScene, false);
        }

        private void PopulateSceneLibrary()
        {
            sceneLibrary.Add(coreScene);
            sceneLibrary.Add(introScene);
            sceneLibrary.Add(menuScene);
            sceneLibrary.Add(sandboxScene);
            sceneLibrary.Add(gameScene);
        }
        #endregion

        private void Start()
        {
            pause = GetComponent<Pause>();
            listenerManager = GetComponent<AudioListenerManager>();
            UpdateListeners(coreScene);
            Activate();
        }

        public AudioListenerManager GetAudioListenerManagerReference() { return listenerManager; }

        #region setups
        private void Activate()
        {
            StartCoroutine(LaunchApplicationAndMenu());
        }
        #endregion

        #region options menu scene
        public bool ShowQuitButton()        //i.e. is the last active scene a gameplay scene
        {
            //if (otherScenes[indexOfLastActiveScene].isGameplayScene)
            if (lastActiveScene != null && lastActiveScene.isGameplayScene)
            {
                return true;
            }
            return false;
        }

        public bool IsMenuSceneActive()
        {
            //if (GetActiveScene().name == otherScenes[SCENEINDEX_MENU].sceneName)
            if (GetActiveScene() == menuScene)
            {
                return true;
            }
            return false;
        }

        public bool IsThereAnotherLiveScene()
        {
            if (lastActiveScene != null && lastActiveScene.isGameplayScene)
            {
                return true;
            }
            return false;
        }

        public void LoadOptionsMenu()
        {
            lastActiveScene = GetActiveScene();
            StartCoroutine(LoadNewSceneAdditive(menuScene, true, false));
        }

        public void CloseOptionsMenu()
        {
            SetActiveScene(lastActiveScene, true);
            lastActiveScene = menuScene;
        }
        #endregion

        #region load gameplay scenes
        public void LoadGameScene()
        {
            if (gameScene.isLoaded)
            {
                SetActiveScene(gameScene, true);
                UpdateListeners(gameScene);
            }
            else
            {
                UnloadAllOtherGameplayScenes(gameScene);
                StartCoroutine(LoadNewSceneAdditive(gameScene, true, true));
            }
            pause.TogglePause(false);
            UnloadScene(menuScene);
        }

        public void LoadSandboxScene()
        {
            if (sandboxScene.isLoaded)
            {
                SetActiveScene(sandboxScene, true);
            }
            else
            {
                UnloadAllOtherGameplayScenes(sandboxScene);
                StartCoroutine(LoadNewSceneAdditive(sandboxScene, true, true));
            }
            pause.TogglePause(false);
            UnloadScene(menuScene);
        }
        #endregion

        #region load application and menu
        private IEnumerator LaunchApplicationAndMenu()
        {
            //yield return LoadNewSceneAdditive(otherScenes[SCENEINDEX_INTRO].sceneName, false);
            //yield return LoadNewSceneAdditive(introSceneDeets.sceneName, false);
            yield return LoadNewSceneAdditive(introScene, true, false);

            yield return new WaitForSeconds(introLoadDelayTime);

            //yield return LoadNewSceneAdditive(otherScenes[SCENEINDEX_MENU].sceneName, true);
            yield return LoadNewSceneAdditive(menuScene, true, false);

            //UnloadScene(SCENEINDEX_INTRO);
            UnloadScene(introScene);
        }
        #endregion

        #region scene controls
        public SceneListItem GetActiveScene()
        {
            Scene scene = SceneManager.GetActiveScene();

            foreach (SceneListItem item in sceneLibrary)
            {
                if (item.sceneName == scene.name)
                {
                    return item;
                }
            }
            Debug.Log("active scene not in library");
            return null;
        }

        public void QuitGameplay()
        {
            if (lastActiveScene.isLoaded)
            {
                UnloadScene(lastActiveScene);
                lastActiveScene = null;
            }
        }

        private IEnumerator LoadNewSceneAdditive(SceneListItem newScene, bool isActive, bool isGameplay)
        {
            if (!newScene.isLoaded)
            {
                AsyncOperation asyncOp;

                asyncOp = SceneManager.LoadSceneAsync(newScene.sceneName, LoadSceneMode.Additive);
                asyncOp.allowSceneActivation = false;

                yield return MonitorLoadingOfScene(asyncOp);    //change status @90%

                asyncOp.allowSceneActivation = true;

                yield return CheckIfLoadIsDone(asyncOp);
            }

            GetSceneAndIndexOfScene(newScene);  //only available when loaded
            SetActiveScene(newScene, isActive);
            SetSceneLoaded(newScene, true);
            SetSceneGameplay(newScene, isGameplay);
            UpdateListeners(newScene);
        }

        private IEnumerator MonitorLoadingOfScene(AsyncOperation asyncScene)
        {
            while (asyncScene.progress < 0.9f)
            {
                yield return null;
            }
        }

        private IEnumerator CheckIfLoadIsDone(AsyncOperation asyncScene)
        {
            while (!asyncScene.isDone)
            {
                yield return null;
            }

        }

        private void GetSceneAndIndexOfScene(SceneListItem item)
        {
            item.scene = SceneManager.GetSceneByName(item.sceneName);
            item.sceneIndex = item.scene.buildIndex;
        }

        private void SetActiveScene(SceneListItem sceneToSet, bool setActive)
        {
            if (setActive)
            {
                SceneManager.SetActiveScene(sceneToSet.scene);
                MarkArrayIndexAsActive(sceneToSet);
            }
            //listenerManager.ParseListeners(otherScenes[index].scene, setActive);
        }

        private void UnloadAllOtherGameplayScenes(SceneListItem sceneToKeep)
        {
            foreach (SceneListItem item in sceneLibrary)
            {
                if (item != sceneToKeep && item.isGameplayScene && item.isLoaded)
                {
                    UnloadScene(item);
                }
            }
        }

        public delegate void OnSceneUnloaded(string sceneName);
        public static event OnSceneUnloaded onSceneUnloaded;

        private void UnloadScene(SceneListItem sceneToUnload)
        {
            onSceneUnloaded?.Invoke(sceneToUnload.sceneName);
            SetSceneLoaded(sceneToUnload, false);
            SceneManager.UnloadSceneAsync(sceneToUnload.sceneName);
        }
        #endregion

        #region list mgmt
        private void SetSceneLoaded(SceneListItem item, bool isLoaded)
        {
            item.isLoaded = isLoaded;
        }

        private void SetSceneGameplay(SceneListItem item, bool isGameplay)
        {
            item.isGameplayScene = isGameplay;
        }

        private void MarkArrayIndexAsActive(SceneListItem activeScene)
        {
            foreach (SceneListItem item in sceneLibrary)
            {
                if (item != activeScene)
                {
                    item.isActive = false;
                }
                else
                {
                    item.isActive = true;
                }
            }
        }
        #endregion

        #region listeners
        private void UpdateListeners(SceneListItem sceneItem)
        {
            listenerManager.UpdateListeners(sceneItem.scene);
        }
        #endregion
    }
}

