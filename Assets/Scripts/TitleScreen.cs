using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour
{
    private VisualElement background;
    private Button start;
    private Button credits;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;
        background = rootVisualElement.Q<VisualElement>("Background");
        start = background.Q<Button>("Start");
        start.RegisterCallback<ClickEvent>(ev => StartGame());
        credits = background.Q<Button>("Credits");
    }

    private void StartGame()
    {
        Debug.Log("start");
        SceneManager.LoadSceneAsync("GameplayScene", LoadSceneMode.Single);
    }
}
