using HarmonyLib;
namespace CursedAmongUs.Source.Tasks
{
	internal class CollectShells
	{
		[HarmonyPatch(typeof(CollectShellsMinigame))]
		private static class CollectShellsMinigameMinigamePatch
		{
			[HarmonyPatch(nameof(CollectShellsMinigame.Begin)), HarmonyPrefix]

			private static void BeginPrefix(CollectShellsMinigame __instance)
			{
				System.Random Rd = new System.Random();

				__instance.numShellsRange = (IntRange)Rd.Next(4,20);

			}
		}


	}
}
