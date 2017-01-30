using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Play 버튼 누르면 Save 해주는 유사 AutoSave Script. 추가 윈도우를 통해 기능을 켜고 끌 수 있다.(유니티 구동 시 기본값은 true)
/// </summary>

[InitializeOnLoad]
static class OnPlaySavor
{
    [MenuItem("Window/OnPlaySavor")]
    public static void ShowWindow()
    {
        // Debug.Log("OnPlaySavor Setting Window");
        EditorWindow.GetWindow(typeof(OnPlaySavorWindow));
    }

    // Constructor이 두번 불리는 기이한 현상이 있어서...
    private static bool isInitialized = false;
    static OnPlaySavor()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            EditorApplication.playmodeStateChanged += OnPlayModeChanged;
            // 뭐 False 일 리는 없겠지만
            Debug.Log("OnPlaySavor Launched : OnPlaySavorMode " + (SaveOnPlay ? "On" : "Off"));
        }
    }

    public static bool SaveOnPlay = true;

    static void OnPlayModeChanged()
    {
        if (SaveOnPlay)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                if (!EditorApplication.isPlaying)
                {
                    EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
                    // Obsolete : 유니티 구버전은 이 메소드를 사용할 것
                    //EditorApplication.SaveScene();
                    Debug.Log("OnPlaySavor : Saved");
                }
            }
        }
        else
        {
            // OnPlaySavor이 켜져있는지 아닌지 확인하고 싶다면 주석을 해제할 것
            // Debug.Log("OnPlaySavor is off");
        }
    }
}

public class OnPlaySavorWindow : EditorWindow
{
    public bool SaveOnPlay
    {
        get
        {
            return OnPlaySavor.SaveOnPlay;
        }
        set
        {
            // OnGUI때 프레임마다 불려대서 그만
            if (OnPlaySavor.SaveOnPlay ^ value)
            {
                OnPlaySavor.SaveOnPlay = value;
                Debug.Log("OnPlaySavor Mode Set : OnPlaySavorMode " + (OnPlaySavor.SaveOnPlay ? "On" : "Off"));
            }
            else
            {
                OnPlaySavor.SaveOnPlay = value;
            }
        }
    }

    // On Gui : Show Menu for SaveOnPlay Mode
    void OnGUI()
    {
        GUILayout.Label("SaveOnPlay Settings", EditorStyles.boldLabel);
        SaveOnPlay = EditorGUILayout.Toggle("Save On Play", SaveOnPlay);
    }
}
