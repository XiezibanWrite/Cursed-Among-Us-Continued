using HarmonyLib;
namespace CursedAmongUs.Source.Tasks
{
	internal class Course
	{
		[HarmonyPatch(typeof(CourseMinigame))]
		private static class CourseMinigamePatch
		{
			[HarmonyPatch(nameof(CourseMinigame.Begin)), HarmonyPrefix]
			private static void BeginPrefix(CourseMinigame __instance)
			{
				System.Random rd = new System.Random();
				__instance.NumPoints = rd.Next(20,27);
			}
		}
	}
}
