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
    static OnPlaySavor()
    {
        EditorApplication.playmodeStateChanged += OnPlayModeChanged;
    }

    static void OnPlayModeChanged()
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
}