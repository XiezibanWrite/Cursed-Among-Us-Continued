
using HarmonyLib;

using UnityEngine;


namespace CursedAmongUs.Source.Tasks
{

	internal class CaliDistributar
	{
		public static GameObject Sweeper => GameObject.Find("Sweeper (1)");
		public static GameObject Sweeper2 => GameObject.Find("Sweeper2 (1)");
		public static GameObject Sweeper3 => GameObject.Find("Sweeper3 (1)");
		public static GameObject calibratorGauge => GameObject.Find("calibratorGauge");
		public static GameObject calibratorGauge2 => GameObject.Find("calibratorGauge (1)");
		public static GameObject calibratorGauge3 => GameObject.Find("calibratorGauge (2)");


		[HarmonyPatch(typeof(SweepMinigame))]
		private static class SweepMinigamePatch
		{
			[HarmonyPatch(nameof(SweepMinigame.Begin))]
			[HarmonyPostfix]
			
			private static void BeginPostfix(SweepMinigame __instance)
			{
				Sweeper.SetActive(false); 
				Sweeper2.SetActive(false);
				Sweeper3.SetActive(false);
				calibratorGauge.SetActive(false);
				calibratorGauge2.SetActive(false);
				calibratorGauge3.SetActive(false);

			}
		}
	}
}
