
using HarmonyLib;



using UnityEngine;


namespace CursedAmongUs.Source.Tasks
{

	internal class CaliDistributar
	{
		[HarmonyPatch(typeof(SweepMinigame))]
		private static class SweepMinigamePatch
		{
			[HarmonyPatch(nameof(SweepMinigame.FixedUpdate)), HarmonyPrefix]

			private static void FixedUpdatePrefix(SweepMinigame __instance)
			{
				System.Random numer = new System.Random();
				float num = (float)numer.Next(10, 50);
				__instance.timer += Time.fixedDeltaTime * num;
			}
		}
		

	}
}
