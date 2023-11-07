
using HarmonyLib;

namespace CursedAmongUs.Source.Tasks
{

	internal class RoastMarshmallowFire
	{
		[HarmonyPatch(typeof(RoastMarshmallowFireMinigame))]
		private static class RoastMarshmallowFireMinigamePatch
		{
			[HarmonyPatch(nameof(RoastMarshmallowFireMinigame.Update)), HarmonyPrefix]

			private static void FixedUpdatePrefix(RoastMarshmallowFireMinigame __instance)
			{
				System.Random rd = new System.Random();
				float timetoast = (float)rd.Next(360,1000);
				__instance.timeToToasted = timetoast;


			}
		}
		

	}
}
