using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using CursedAmongUs.Source.Tasks;
using CursedAmongUs.Source;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Reactor;
using Reactor.Utilities;
using UnityEngine;

namespace CursedAmongUs;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
[BepInDependency(ReactorPlugin.Id)]
public partial class CursedAmongUs : BasePlugin
{
    public Harmony Harmony { get; } = new(Id);

    public ConfigEntry<System.String> ConfigName { get; private set; }

    public override void Load()
    {
       
        Harmony.PatchAll();
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class ExamplePatch
    {
        public static void Postfix(PlayerControl __instance)
        {
            

            GameObject gameObject = GameObject.Find("CursedAmongUs");
            if (gameObject != null) return;
            ClassInjector.RegisterTypeInIl2Cpp<CursedGameData>();
            ClassInjector.RegisterTypeInIl2Cpp<CursedWeapons.WeaponsCustom>();
            ClassInjector.RegisterTypeInIl2Cpp<UploadDataCustom>();
            GameObject cursedObject = new("CursedAmongUs");
            Object.DontDestroyOnLoad(cursedObject);
            _ = cursedObject.AddComponent<CursedGameData>();
        }
    }
}
