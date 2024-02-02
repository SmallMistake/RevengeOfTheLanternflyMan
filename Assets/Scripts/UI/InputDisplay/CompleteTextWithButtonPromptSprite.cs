using MoreMountains.TopDownEngine;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public static class CompleteTextWithButtonPromptSprite
{
    // capture multiple counts of input sprite requests
    private static string ACTION_PATTERN = @"\{(.*?)}";
    private static Regex REGEX = new Regex(ACTION_PATTERN, RegexOptions.IgnoreCase);

    /// <summary>
    /// Add Active Device to sub call
    /// </summary>
    /// <param name="textWithActions"></param>
    /// <param name="inputDeviceManager"></param>
    /// <param name="spriteAssetDefinitions"></param>
    /// <returns></returns>
    public static string ReplaceActiveBindings(string textWithActions, InputDeviceManager inputDeviceManager, SpriteAssetDefinitions spriteAssetDefinitions)
    {
        return ReplaceBindings(textWithActions, inputDeviceManager.GetActiveDevice(), inputDeviceManager, spriteAssetDefinitions);
    }

    /// <summary>
    /// Use string and replace Event tokens with sprite images.
    /// </summary>
    /// <param name="textWithActions"></param>
    /// <param name="deviceType"></param>
    /// <param name="inputDeviceManager"></param>
    /// <param name="spriteAssetDefinitions"></param>
    /// <returns></returns>
    public static string ReplaceBindings(string textWithActions, DynamicDeviceType deviceType, InputDeviceManager inputDeviceManager, SpriteAssetDefinitions spriteAssetDefinitions)
    {

        MatchCollection matches = REGEX.Matches(textWithActions);
        // original template
        var replacedText = textWithActions;
        foreach (Match match in matches)
        {
            var withBraces = match.Groups[0].Captures[0].Value;
            var innerPart = match.Groups[1].Captures[0].Value;

            var tagText = GetSpriteTag(innerPart, deviceType, inputDeviceManager, spriteAssetDefinitions);

            replacedText = replacedText.Replace(withBraces, tagText);
        }

        return replacedText;
    }

    /// <summary>
    /// Look up the InputBinding based on device type and returns the TextMeshPro sprite tag
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name=""></param>
    /// <param name="inputDeviceManager"></param>
    /// <param name="spriteAssets"></param>
    /// <returns></returns>
    public static string GetSpriteTag(string actionName, DynamicDeviceType deviceType, InputDeviceManager inputDeviceManager, SpriteAssetDefinitions spriteAssets)
    {
        InputBinding dynamicBinding = inputDeviceManager.GetBinding(actionName, deviceType, inputDeviceManager.GetPlayerInput());
        TMP_SpriteAsset spriteAsset = spriteAssets.spriteAssets[(int)deviceType];

        //Debug.LogFormat("Retrieving sprite tag for: {0} with the path {1}", dynamicBinding.action, dynamicBinding.effectivePath);
        string stringButtonName = dynamicBinding.effectivePath;
        stringButtonName = RenameInput(stringButtonName);
        return $"<sprite=\"{spriteAsset.name}\" name=\"{stringButtonName}\">";

    }

    private static string RenameInput(string stringButtonName)
    {
        stringButtonName = stringButtonName.Replace(
            "<Keyboard>/", "Key_");
        stringButtonName = stringButtonName.Replace(
            "<Gamepad>/", "Gamepad_");
        stringButtonName = stringButtonName.Replace(
            "<SwitchProControllerHID>/", "Switch_");
        return stringButtonName;
    }
}
