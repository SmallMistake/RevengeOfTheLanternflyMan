using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TMP_Text))]
public class SetTextToTextBox : MonoBehaviour
{
    [TextArea(2, 3)]
    [SerializeField]
    private string message;

    [Header("Setup for Sprites")]
    [SerializeField]
    private SpriteAssetDefinitions spriteAssetDefinitions;
    [SerializeField]
    private InputDeviceManager inputDeviceManager;

    [SerializeField]
    private TMP_Text _textBox;

    private void Awake()
    {
        //playerInput = new PlayerInput();
        inputDeviceManager = InputDeviceManager.Instance;
        _textBox = GetComponent<TMP_Text>();
        inputDeviceManager.ActiveDeviceChangeEvent += SetText;
    }

    private void OnDestroy()
    {
        inputDeviceManager.ActiveDeviceChangeEvent -= SetText;
    }

    private void OnEnable()
    {
        SetText();
    }

    [ContextMenu("Set Text")]
    private void SetText()
    {
        _textBox.text = CompleteTextWithButtonPromptSprite.ReplaceActiveBindings(message, inputDeviceManager, spriteAssetDefinitions);
    }
}
