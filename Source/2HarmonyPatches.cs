// Decompiled with JetBrains decompiler
// Type: StopDropAndRoll.Patch_CanEverAttachFire
// Assembly: StopDropAndRoll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E2A5F47-616E-450E-8FEB-EB49A677CD5C
// Assembly location: L:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\StopDropAndRoll\1.4\Assemblies\StopDropAndRoll.dll

using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace StopDropAndRoll
{
  [HarmonyPatch(typeof (FireUtility), "TryAttachFire")]
  public static class Patch_CanEverAttachFire
  {
    private static bool Prefix(Thing t)
    {
      Pawn pawn = t as Pawn;
      if (pawn != null && (StopDropAndRollMod.settings.enableCustomFireAttachOnColonists || !pawn.get_RaceProps().get_Humanlike() || !pawn.get_IsColonist()) && ((StopDropAndRollMod.settings.enableCustomFireAttachOnNonColonists || !pawn.get_RaceProps().get_Humanlike() || pawn.get_IsColonist()) && (StopDropAndRollMod.settings.enableCustomFireAttachOnAnimals || !pawn.get_RaceProps().get_Animal())))
      {
        BodyPartRecord bodyPartRecord = (BodyPartRecord) GenCollection.RandomElementByWeight<BodyPartRecord>((IEnumerable<M0>) ((IEnumerable<BodyPartRecord>) ((BodyDef) pawn.get_RaceProps().body).get_AllParts()).Where<BodyPartRecord>((Func<BodyPartRecord, bool>) (x => !((HediffSet) ((Pawn_HealthTracker) pawn.health).hediffSet).PartIsMissing(x) && x.depth == 2 && (!((List<BodyPartGroupDef>) x.groups).Contains((BodyPartGroupDef) BodyPartGroupDefOf.LeftHand) && !((List<BodyPartGroupDef>) x.groups).Contains((BodyPartGroupDef) BodyPartGroupDefOf.RightHand)) && (x.def != BodyPartDefOf.Hand && !((List<BodyPartGroupDef>) x.groups).Contains(DefDatabase<BodyPartGroupDef>.GetNamed("Feet", true))) && !((List<BodyPartGroupDef>) x.groups).Contains((BodyPartGroupDef) BodyPartGroupDefOf.Legs))), (Func<M0, float>) (y => (float) y.coverage));
        List<Apparel> list = ((IEnumerable<Apparel>) ((IEnumerable<Apparel>) Patch_CanEverAttachFire.ApparelsOnBodyPart(pawn, bodyPartRecord)).OrderByDescending<Apparel, int>((Func<Apparel, int>) (x => (int) ((ApparelProperties) ((ThingDef) ((Thing) x).def).apparel).get_LastLayer().drawOrder))).ToList<Apparel>();
        for (int index = 0; index < list.Count; ++index)
        {
          if (!Rand.Chance(Patch_CanEverAttachFire.GetApparelIgnitionChance(StatExtension.GetStatValue((Thing) list[index], (StatDef) StatDefOf.ArmorRating_Heat, true))))
            return false;
          if (index == list.Count - 1)
            return true;
        }
      }
      return true;
    }

    public static float GetApparelIgnitionChance(float apparelHeatRating)
    {
      return (float) (1.0 - (double) StopDropAndRollMod.settings.IRS * ((double) apparelHeatRating - (double) StopDropAndRollMod.settings.MHA));
    }

    public static List<Apparel> ApparelsOnBodyPart(
      Pawn pawn,
      BodyPartRecord bodyPartRecord)
    {
      List<Apparel> apparelList = new List<Apparel>();
      if (((Pawn_ApparelTracker) pawn.apparel)?.get_WornApparel() != null)
      {
        using (List<BodyPartGroupDef>.Enumerator enumerator = ((List<BodyPartGroupDef>) bodyPartRecord.groups).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            BodyPartGroupDef current = enumerator.Current;
            for (int index1 = 0; index1 < ((Pawn_ApparelTracker) pawn.apparel).get_WornApparel().Count; ++index1)
            {
              Apparel apparel = ((Pawn_ApparelTracker) pawn.apparel).get_WornApparel()[index1];
              for (int index2 = 0; index2 < ((List<BodyPartGroupDef>) ((ApparelProperties) ((ThingDef) ((Thing) apparel).def).apparel).bodyPartGroups).Count; ++index2)
              {
                if (((List<BodyPartGroupDef>) ((ApparelProperties) ((ThingDef) ((Thing) apparel).def).apparel).bodyPartGroups)[index2] == current && !apparelList.Contains(apparel))
                  apparelList.Add(apparel);
              }
            }
          }
        }
      }
      return apparelList;
    }
  }
}
