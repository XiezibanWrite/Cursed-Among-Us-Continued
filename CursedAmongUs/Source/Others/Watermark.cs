//using System;
//using AmongUs.GameOptions;
//using HarmonyLib;
//using UnityEngine;

//namespace CursedAmongUs.Source.Others
//{
//	[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetTasks))]
//	public static class CursedWatermark
//	{
//		private const String WatermarkText = 
//			"<size=130%>Cursed Among Us</size>\n" +
//			"Available at github.com/Devs-Us/Cursed-Among-Us\n";

//		[HarmonyPostfix]
//		private static void Patch()
//		{
//			PlayerControl player = PlayerControl.LocalPlayer;
//			if (player is null )
//				return;

//			ImportantTextTask watermarkText = 
//				new GameObject("CursedWatermark").AddComponent<ImportantTextTask>();

//			watermarkText.transform.SetParent(player.transform, false);
//			watermarkText.Text = $"</color>{WatermarkText}</color>";

//			player.myTasks.Insert(0, watermarkText);
//		}
//	}
//}