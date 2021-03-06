using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.InGame.ActionMenu;
using Assets.Scripts.Unity.UI_New.InGame.RightMenu;
using BetterAutoStart;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using MelonLoader;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[assembly: MelonInfo(typeof(BetterAutoStartMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace BetterAutoStart;

public class BetterAutoStartMod : BloonsTD6Mod
{
    private static void UpdateTextures(GoFastForwardToggle toggle)
    {
        if (InGame.instance.UnityToSimulation.simulation.autoPlay)
        {
            toggle.goImage.GetComponent<Image>()
                .SetSprite(ModContent.GetSprite<BetterAutoStartMod>("GoBtn")!);
            toggle.fastForwardOffImage.GetComponent<Image>()
                .SetSprite(ModContent.GetSprite<BetterAutoStartMod>("FastForwardBtn")!);
            toggle.fastForwardOnImage.GetComponent<Image>()
                .SetSprite(ModContent.GetSprite<BetterAutoStartMod>("FastForwardGlowBtn")!);
        }
        else
        {
            toggle.goImage.GetComponent<Image>().SetSprite(VanillaSprites.GoBtn);
            toggle.fastForwardOffImage.GetComponent<Image>().SetSprite(VanillaSprites.FastForwardBtn);
            toggle.fastForwardOnImage.GetComponent<Image>().SetSprite(VanillaSprites.FastForwardGlowBtn);
        }
    }

    [HarmonyPatch(typeof(UnityToSimulation), nameof(UnityToSimulation.ToggleAutoPlay))]
    internal static class UnityToSimulation_ToggleAutoPlay
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            if (ShopMenu.instance != null && ShopMenu.instance.goFFToggle != null)
            {
                UpdateTextures(ShopMenu.instance.goFFToggle);
            }
        }
    }

    [HarmonyPatch(typeof(GoFastForwardToggle), nameof(GoFastForwardToggle.OnEnable))]
    internal static class GoFastForwardToggle_OnEnable
    {
        [HarmonyPostfix]
        private static void Postfix(GoFastForwardToggle __instance)
        {
            UpdateTextures(__instance);
        }
    }

    [HarmonyPatch(typeof(Button), nameof(Button.OnPointerClick))]
    internal class Button_OnPointerClick
    {
        [HarmonyPostfix]
        internal static void Postfix(Button __instance, PointerEventData eventData)
        {
            if (InGame.instance != null &&
                eventData.button == PointerEventData.InputButton.Right &&
                __instance.name == "FastFoward-Go") //yes this is a real typo in the name
            {
                InGame.instance.bridge.ToggleAutoPlay(!InGame.instance.UnityToSimulation.simulation.autoPlay);
            }
        }
    }
}