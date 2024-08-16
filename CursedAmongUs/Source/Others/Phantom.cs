using HarmonyLib;
namespace CursedAmongUs.Source.Others
{
	internal class Phantom
	{
		[HarmonyPatch(typeof(KillButton))]
		public static class KillButtonForPhantomPatch
		{
			public static bool IsShowPhantomButton = true;
			[HarmonyPatch(nameof(KillButton.DoClick)), HarmonyPostfix]
			private static void ClickPostFix(KillButton __instance)
			{
				if(PlayerControl.LocalPlayer.Data.RoleType == AmongUs.GameOptions.RoleTypes.Phantom)
				{
					IsShowPhantomButton = false;
					_ = new LateTask(() =>
					{
						IsShowPhantomButton = true;
					}, PlayerControl.LocalPlayer.killTimer);
				}

			}
		}

		[HarmonyPatch(typeof(AbilityButton))]
		private static class AbilityButtonForPhantomPatch
		{
			[HarmonyPatch(nameof(AbilityButton.Update)), HarmonyPostfix]
			private static void ClickPostFix(KillButton __instance)
			{
				if (KillButtonForPhantomPatch.IsShowPhantomButton == false)
				{
					__instance.gameObject.SetActive(false);
				}
				else if(KillButtonForPhantomPatch.IsShowPhantomButton == true)
				{
					__instance.gameObject.SetActive(true);
				}

			}
		}
	}
}
