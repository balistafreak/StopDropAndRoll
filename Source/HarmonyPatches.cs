// Decompiled with JetBrains decompiler
// Type: StopDropAndRoll.HarmonyInit
// Assembly: StopDropAndRoll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E2A5F47-616E-450E-8FEB-EB49A677CD5C
// Assembly location: L:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\StopDropAndRoll\1.4\Assemblies\StopDropAndRoll.dll

using HarmonyLib;
using Verse;

namespace StopDropAndRoll
{
  [StaticConstructorOnStartup]
  internal static class HarmonyInit
  {
    static HarmonyInit()
    {
      new Harmony("balistafreak.StopDropAndRoll").PatchAll();
    }
  }
}
