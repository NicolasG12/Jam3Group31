using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.ReorderableList;

public class Credits : MonoBehaviour
{
    public GameObject title;
    private VisualElement background;
    private Button back;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;
        background = rootVisualElement.Q<VisualElement>("Credits");
        back = background.Q<Button>("Back");
        back.RegisterCallback<ClickEvent>(ev => Back());
    }

    private void Back()
    {
        title.SetActive(true);
    }
}
