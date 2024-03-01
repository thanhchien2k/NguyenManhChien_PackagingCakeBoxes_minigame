#define TAPTIC

using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework
{
    public class PGameMaster : MonoBehaviour
    {
        public static event Action<bool> OnGamePaused;
        public static event Action OnGameQuit;
        public static event Action OnClearData;
        public static event Action OnSceneChanged;

        #region Runtime Init

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            GameObject obj = new GameObject(typeof(PGameMaster).ToString());
            obj.AddComponent<PGameMaster>();

//#if UNITY_EDITOR
//            obj.AddComponent<PQuickAction>();
//#endif
            InitVibration();
        }

        #endregion

        #region MonoBehaviour

        void Awake()
        {
#if UNITY_IOS
            if (UnityEngine.iOS.Device.lowPowerModeEnabled)
                Application.targetFrameRate = 30;
            else
                Application.targetFrameRate = 60;
#else
            Application.targetFrameRate = 200;
#endif

            DontDestroyOnLoad(gameObject);

            SceneManager.activeSceneChanged += SceneManager_ActiveSceneChanged;
        }

        void OnApplicationPause(bool pause)
        {
            OnGamePaused?.Invoke(pause);
        }

        void OnApplicationQuit()
        {
            OnGameQuit?.Invoke();
        }

        #endregion

        #region Init

        static void InitVibration()
        {
//#if TAPTIC
//            Taptic.Taptic.tapticOn = PDataSettings.VibrationEnabled;

//            PDataSettings.VibrationEnabledData.OnDataChanged += (enabled) => { Taptic.Taptic.tapticOn = enabled; };
//#endif
        }

        #endregion

        #region Public Static

        public static void ClearData()
        {
            if (!Application.isPlaying)
            OnClearData?.Invoke();
        }

        #endregion

        void SceneManager_ActiveSceneChanged(Scene arg0, Scene arg1)
        {
            OnSceneChanged?.Invoke();
        }
    }
}