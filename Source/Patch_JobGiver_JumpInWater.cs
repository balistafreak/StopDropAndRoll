// Decompiled with JetBrains decompiler
// Type: StopDropAndRoll.Patch_JobGiver_JumpInWater
// Assembly: StopDropAndRoll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E2A5F47-616E-450E-8FEB-EB49A677CD5C
// Assembly location: L:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\StopDropAndRoll\1.4\Assemblies\StopDropAndRoll.dll

using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace StopDropAndRoll
{
  [HarmonyPatch(typeof (JobGiver_JumpInWater), "TryGiveJob")]
  public static class Patch_JobGiver_JumpInWater
  {
    private static bool Prefix(Pawn pawn, ref Job __result)
    {
      return (!StopDropAndRollMod.settings.enableStopDropAndRollOnColonists || !pawn.get_RaceProps().get_Humanlike() || !pawn.get_IsColonist()) && (!StopDropAndRollMod.settings.enableStopDropAndRollOnNonColonists || !pawn.get_RaceProps().get_Humanlike() || pawn.get_IsColonist()) && (!StopDropAndRollMod.settings.enableStopDropAndRollOnAnimals || !pawn.get_RaceProps().get_Animal());
    }
  }
}
