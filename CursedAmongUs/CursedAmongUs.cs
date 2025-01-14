﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using CursedAmongUs.Source.Tasks;
using CursedAmongUs.Source;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
namespace CursedAmongUs;

[BepInAutoPlugin]
[BepInProcess("Among Us.exe")]
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
		}
	}
	[HarmonyPatch(typeof(ModManager), nameof(ModManager.LateUpdate))]
	class ModManagerLateUpdatePatch
	{
		public static void Prefix(ModManager __instance)
		{
			__instance.ShowModStamp();
			LateTask.Update(Time.fixedDeltaTime / 2);

		}
	}
}
