using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace StopDropAndRoll
{
	[StaticConstructorOnStartup]
	internal static class HarmonyInit
	{
		static HarmonyInit()
		{
			Harmony harmony = new Harmony("balistafreak.StopDropAndRoll");
			harmony.PatchAll();
		}
	}

	[HarmonyPatch(typeof(JobGiver_ExtinguishSelf), "TryGiveJob")]
	public static class Patch_TryGiveJob
	{
		private static bool Prefix(Pawn pawn, ref Job __result)
		{
			if (!StopDropAndRollMod.settings.enableStopDropAndRollOnColonists && pawn.RaceProps.Humanlike && pawn.IsColonist)
            {
                Log.Message("Fail 1");

                return true;
			}
			if (!StopDropAndRollMod.settings.enableStopDropAndRollOnNonColonists && pawn.RaceProps.Humanlike && !pawn.IsColonist)
            {
                Log.Message("Fail 2");

                return true;
			}
			if (!StopDropAndRollMod.settings.enableStopDropAndRollOnAnimals && pawn.RaceProps.Animal)
			{
				Log.Message("Fail 3");
				return true;
			}

			Fire fire = (Fire)pawn.GetAttachment(ThingDefOf.Fire);
			if (fire != null)
			{
				__result = JobMaker.MakeJob(JobDefOf.ExtinguishSelf, fire);
				return false;
			}
			return true;
		}
	}

	[HarmonyPatch(typeof(FireUtility), "TryAttachFire")]
	public static class Patch_CanEverAttachFire
	{
		private static bool Prefix(Thing t)
		{
			if (t is Pawn pawn)
			{
				if (!StopDropAndRollMod.settings.enableCustomFireAttachOnColonists && pawn.RaceProps.Humanlike && pawn.IsColonist)
                {
					return true;
                }
				if (!StopDropAndRollMod.settings.enableCustomFireAttachOnNonColonists && pawn.RaceProps.Humanlike && !pawn.IsColonist)
                {
					return true;
                }
				if (!StopDropAndRollMod.settings.enableCustomFireAttachOnAnimals && pawn.RaceProps.Animal)
                {
					return true;
                }

				var bodyPartToSetOnFire = (from x in pawn.RaceProps.body.AllParts
								where !pawn.health.hediffSet.PartIsMissing(x)
								&& x.depth == BodyPartDepth.Outside
								&& !x.groups.Contains(BodyPartGroupDefOf.LeftHand)
								&& !x.groups.Contains(BodyPartGroupDefOf.RightHand)
								&& x.def != BodyPartDefOf.Hand
								&& !x.groups.Contains(DefDatabase<BodyPartGroupDef>.GetNamed("Feet"))
								&& !x.groups.Contains(BodyPartGroupDefOf.Legs) select x).RandomElementByWeight(y => y.coverage);
				var apparels = ApparelsOnBodyPart(pawn, bodyPartToSetOnFire).OrderByDescending(x => x.def.apparel.LastLayer.drawOrder).ToList();
				//Log.Message(pawn + " is about to get fire on " + bodyPartToSetOnFire + ", apparel count: " + apparels.Count());
				for (int i = 0; i < apparels.Count; i++)
				{
					var armorHeatRating = apparels[i].GetStatValue(StatDefOf.ArmorRating_Heat);
					var ignitionChance = GetApparelIgnitionChance(armorHeatRating);
					//Log.Message("Checking: " + apparels[i].def.defName + " - ignitionChance: " + ignitionChance + " - armorHeatRating: " + armorHeatRating);
					if (Rand.Chance(ignitionChance))
                    {
						if (i == apparels.Count - 1)
                        {
							//Log.Message(pawn + " last apparel layer was checked, the pawn will set on fire");
							return true;
						}
						//Log.Message("Ignition chance succeeded, going to next apparel");
						continue;
                    }
					else
                    {
						//Log.Message(pawn + " ignition chance failed on " + apparels[i].def.defName + ", the fire was deflected");
						return false;;
                    }
				}
			}
			return true;
		}

		public static float GetApparelIgnitionChance(float apparelHeatRating)
        {
			return 1f - StopDropAndRollMod.settings.IRS * (apparelHeatRating - StopDropAndRollMod.settings.MHA);
		}

		public static List<Apparel> ApparelsOnBodyPart(Pawn pawn, BodyPartRecord bodyPartRecord)
		{
			List<Apparel> coveredApparels = new List<Apparel>();
			if (pawn.apparel?.WornApparel != null)
            {
				foreach (var group in bodyPartRecord.groups)
                {
					for (int i = 0; i < pawn.apparel.WornApparel.Count; i++)
					{
						Apparel apparel = pawn.apparel.WornApparel[i];
						for (int j = 0; j < apparel.def.apparel.bodyPartGroups.Count; j++)
						{
							if (apparel.def.apparel.bodyPartGroups[j] == group)
							{
								if (!coveredApparels.Contains(apparel))
                                {
									coveredApparels.Add(apparel);
                                }
							}
						}
					}
				}
			}
			return coveredApparels;
		}
	}
}
