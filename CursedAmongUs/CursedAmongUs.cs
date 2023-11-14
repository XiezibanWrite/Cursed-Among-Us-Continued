using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Reactor;
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
			GameObject cursedObject = new("CursedAmongUs");
			Object.DontDestroyOnLoad(cursedObject);
		}
	}
}
