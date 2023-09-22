// Decompiled with JetBrains decompiler
// Type: StopDropAndRoll.StopDropAndRollMod
// Assembly: StopDropAndRoll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E2A5F47-616E-450E-8FEB-EB49A677CD5C
// Assembly location: L:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\StopDropAndRoll\1.4\Assemblies\StopDropAndRoll.dll

using UnityEngine;
using Verse;

namespace StopDropAndRoll
{
  public class StopDropAndRollMod : Mod
  {
    public static StopDropAndRollSettings settings;

    public StopDropAndRollMod(ModContentPack pack)
    {
      base.\u002Ector(pack);
      StopDropAndRollMod.settings = (StopDropAndRollSettings) this.GetSettings<StopDropAndRollSettings>();
    }

    public virtual void DoSettingsWindowContents(Rect inRect)
    {
      base.DoSettingsWindowContents(inRect);
      StopDropAndRollMod.settings.DoSettingsWindowContents(inRect);
    }

    public virtual string SettingsCategory()
    {
      return "Stop Drop And Roll";
    }
  }
}
