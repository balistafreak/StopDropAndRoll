// Decompiled with JetBrains decompiler
// Type: StopDropAndRoll.Patch_TryGiveJob
// Assembly: StopDropAndRoll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E2A5F47-616E-450E-8FEB-EB49A677CD5C
// Assembly location: L:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\StopDropAndRoll\1.4\Assemblies\StopDropAndRoll.dll

using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace StopDropAndRoll
{
  [HarmonyPatch(typeof (JobGiver_ExtinguishSelf), "TryGiveJob")]
  public static class Patch_TryGiveJob
  {
    private static bool Prefix(Pawn pawn, ref Job __result)
    {
      if (!StopDropAndRollMod.settings.enableStopDropAndRollOnColonists && pawn.get_RaceProps().get_Humanlike() && pawn.get_IsColonist() || !StopDropAndRollMod.settings.enableStopDropAndRollOnNonColonists && pawn.get_RaceProps().get_Humanlike() && !pawn.get_IsColonist() || !StopDropAndRollMod.settings.enableStopDropAndRollOnAnimals && pawn.get_RaceProps().get_Animal())
        return true;
      Fire attachment = (Fire) AttachmentUtility.GetAttachment((Thing) pawn, (ThingDef) ThingDefOf.Fire);
      if (attachment == null)
        return true;
      __result = JobMaker.MakeJob((JobDef) JobDefOf.ExtinguishSelf, LocalTargetInfo.op_Implicit((Thing) attachment));
      return false;
    }
  }
}
