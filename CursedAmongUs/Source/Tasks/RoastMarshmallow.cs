
using HarmonyLib;

namespace CursedAmongUs.Source.Tasks
{

	internal class RoastMarshmallowFire
	{
		[HarmonyPatch(typeof(RoastMarshmallowFireMinigame))]
		private static class RoastMarshmallowFireMinigamePatch
		{
			[HarmonyPatch(nameof(RoastMarshmallowFireMinigame.Begin)), HarmonyPrefix]

			private static void BeginPrefix(RoastMarshmallowFireMinigame __instance)
			{
				System.Random rd = new System.Random();
				float timetoast = (float)rd.Next(120,300);
				__instance.timeToToasted = timetoast;


			}
		}
		

	}
}
