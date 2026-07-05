using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using CursedAmongUs.Modules;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace CursedAmongUs.Source
{
	[HarmonyPatch]
	public class CredentialsPatch
	{
		[HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
		public static class PingTrackerPatch
		{
			static void Postfix(PingTracker __instance)
			{
				__instance.text.alignment = TextAlignmentOptions.Top;
				var position = __instance.GetComponent<AspectPosition>();
				position.Alignment = AspectPosition.EdgeAlignments.Top;

				string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
				string moddedBy = ModTranslation.GetString("moddedBy");
				string continuedBy = ModTranslation.GetString("continuedBy", "Continued by {0}",
					"<color=#FFFFE0>Among us</color> & <color=#00FFFF>HayashiUme</color>");
				string ping = ModTranslation.GetString("ping", "Ping: {0} ms", AmongUsClient.Instance.Ping);

				__instance.text.text = $"<color=#ff351f>CursedAmongUs</color> v{version}\n{moddedBy}\n{continuedBy}\n{ping}";
				if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started)
				{
					position.DistanceFromEdge = new Vector3(0f, 0.1f, 0);
				}
				position.AdjustPosition();
			}
		}
	}
	[HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
	public static class LogoPatch
	{
		public static SpriteRenderer renderer;
		private static PingTracker instance;
		public static bool moddedMainMenu = false;
		static void Postfix(PingTracker __instance)
		{
			if (moddedMainMenu)
			{
				var cauLogo = new GameObject("bannerLogo_CAU");
				cauLogo.transform.localPosition = new Vector3(2.0491f, 0.8f, 5f);
				cauLogo.transform.localScale = new Vector3(3.4527f, 2.8873f, 1f);
				renderer = cauLogo.AddComponent<SpriteRenderer>();
				renderer.sprite = PictureLoad.LoadSprite("CursedAmongUs.Resources.Banner.png", 300f);


				instance = __instance;


				var credentialObject = new GameObject("credentialsCAU");
				var credentials = credentialObject.AddComponent<TextMeshPro>();
				credentials.SetText($"<color=#ff351f>CursedAmongUs</color> v{Assembly.GetExecutingAssembly().GetName().Version}\nModded by Devs-Us \nContinued by <color=#FFFFE0>Among us(XiezibanWrite)</color>");
				credentials.alignment = TMPro.TextAlignmentOptions.Center;
				credentials.fontSize *= 0.05f;
				credentials.transform.localPosition = new Vector3(2.0036f, -1f, 5f);
			}
		}
	}
	public static class PictureLoad
	{
		public static Dictionary<string, Sprite> CachedSprites = new();
		public static Sprite LoadSprite(string path, float pixelsPerUnit = 1f)
		{
			try
			{
				if (CachedSprites.TryGetValue(path + pixelsPerUnit, out var sprite)) return sprite;
				Texture2D texture = LoadTextureFromResources(path);
				sprite = Sprite.Create(texture, new(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
				sprite.hideFlags |= HideFlags.HideAndDontSave | HideFlags.DontSaveInEditor;
				return CachedSprites[path + pixelsPerUnit] = sprite;
			}
			catch
			{
				// ignored
			}

			return null;
		}
		public static Texture2D LoadTextureFromResources(string path)
		{
			var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
			var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
			using MemoryStream ms = new();
			stream.CopyTo(ms);
			ImageConversion.LoadImage(texture, ms.ToArray(), false);
			return texture;
		}
	}
	[HarmonyPatch(typeof(IL2CPPChainloader), nameof(IL2CPPChainloader.LoadPlugin))]
	public static class DisableOtherPlugins
	{
		public static void Postfix([HarmonyArgument(0)] PluginInfo pluginInfo, [HarmonyArgument(1)] Assembly pluginAssembly)
		{
			if(pluginInfo.Metadata.GUID == "com.sinai.unityexplorer" || pluginInfo.Metadata.GUID == "gg.reactor.api")
			{
				LogoPatch.moddedMainMenu = true;
			}
		}
	}
}
