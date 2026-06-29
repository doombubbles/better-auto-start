using BetterAutoStart;
using BTD_Mod_Helper;
using MelonLoader;

[assembly: MelonInfo(typeof(BetterAutoStartMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6-Epic")]

namespace BetterAutoStart;

/// <summary>
/// <see cref="BetterAutoStartUtility"/>
/// </summary>
public class BetterAutoStartMod : BloonsTD6Mod;