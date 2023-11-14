using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CursedAmongUs.Source.Tasks;

public class CursedVentCleaningTask
{
	[HarmonyPatch(typeof(VentCleaningMinigame))]
	public static class VentCleaningMinigamePatch
	{
		[HarmonyPatch(nameof(VentCleaningMinigame.Begin)), HarmonyPostfix]
		public static void BeginPostfix(VentCleaningMinigame __instance)
		{

			Transform TaskParent = __instance.transform.parent;
			for (int i = 0; i < TaskParent.childCount; i++)
			{
				Transform child = TaskParent.GetChild(i);
				if (child.name == "VentDirt(Clone)") Object.Destroy(child);
			}
			System.Random Rd = new System.Random();
			__instance.numberOfDirts = Rd.Next(500, 800);
			for (int i = 0; i < __instance.numberOfDirts; i++) __instance.SpawnDirt();
		}
	}
}