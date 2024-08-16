using HarmonyLib;
using UnityEngine;
namespace CursedAmongUs.Source.Tasks
{
	internal class Towel
	{
		[HarmonyPatch(typeof(TowelMinigame))]
		private static class TowelMinigamePatch
		{
			[HarmonyPatch(nameof(TowelMinigame.Begin)), HarmonyPostfix]
			private static void BeginPostfix(TowelMinigame __instance)
			{
				Collider2D copytowel = Object.Instantiate(__instance.Towels[0]);
				copytowel.gameObject.SetActive(false);
				Vector3 scale = copytowel.transform.localScale;
				scale.x -= 0.1f;
				scale.y -= 0.1f;
				copytowel.transform.localScale = scale;
				foreach (Collider2D towel in __instance.Towels)
					Object.Destroy(towel.gameObject);
				__instance.Towels = new Collider2D[25];
				for (int i = 0; i < 25; i++)
				{
					Collider2D towel = Object.Instantiate(copytowel);
					towel.transform.SetParent(__instance.transform);
					towel.transform.position = __instance.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
					towel.name = $"towel_towel(Clone) {i}";
					__instance.Towels[i] = towel;
					towel.gameObject.SetActive(true);
				}
				Object.Destroy(copytowel.gameObject);

			}
		}


	}
}
