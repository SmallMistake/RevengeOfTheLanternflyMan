using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SetTextToTextBox : MonoBehaviour, MMEventListener<RebindEvent>
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

    [Header("Force Device Type")]
    //Optional way to make the visual only show as one controller type
    [SerializeField]
    private bool shouldForceDeviceType;
    [SerializeField]
    private DynamicDeviceType deviceTypeToUse;

    private void Awake()
    {
        //playerInput = new PlayerInput();
        inputDeviceManager = InputDeviceManager.Instance;
        _textBox = GetComponent<TMP_Text>();
        inputDeviceManager.ActiveDeviceChangeEvent += SetText;
    }

    private void OnEnable()
    {
        this.MMEventStartListening<RebindEvent>();
        SetText();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<RebindEvent>();
    }

    private void OnDestroy()
    {
        inputDeviceManager.ActiveDeviceChangeEvent -= SetText;
    }

    public void OnMMEvent(RebindEvent eventType)
    {
        SetText();
    }

    [ContextMenu("Set Text")]
    private void SetText()
    {
        if(shouldForceDeviceType)
        {
            _textBox.text = CompleteTextWithButtonPromptSprite.ReplaceBindings(message, deviceTypeToUse, inputDeviceManager, spriteAssetDefinitions);
        }
        else
        {
            _textBox.text = CompleteTextWithButtonPromptSprite.ReplaceActiveBindings(message, inputDeviceManager, spriteAssetDefinitions);
        }
    }
}
