// Decompiled with JetBrains decompiler
// Type: StopDropAndRoll.StopDropAndRollSettings
// Assembly: StopDropAndRoll, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E2A5F47-616E-450E-8FEB-EB49A677CD5C
// Assembly location: L:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\StopDropAndRoll\1.4\Assemblies\StopDropAndRoll.dll

using UnityEngine;
using Verse;

namespace StopDropAndRoll
{
  public class StopDropAndRollSettings : ModSettings
  {
    public float IRS;
    public float MHA;
    public bool enableStopDropAndRollOnColonists;
    public bool enableStopDropAndRollOnNonColonists;
    public bool enableStopDropAndRollOnAnimals;
    public bool enableCustomFireAttachOnColonists;
    public bool enableCustomFireAttachOnNonColonists;
    public bool enableCustomFireAttachOnAnimals;
    private string buff1;
    private string buff2;

    public virtual void ExposeData()
    {
      base.ExposeData();
      // ISSUE: cast to a reference type
      Scribe_Values.Look<float>((M0&) ref this.IRS, "IRS", (M0) 4.0, false);
      // ISSUE: cast to a reference type
      Scribe_Values.Look<float>((M0&) ref this.MHA, "MHA", (M0) 0.25, false);
      // ISSUE: cast to a reference type
      Scribe_Values.Look<bool>((M0&) ref this.enableStopDropAndRollOnColonists, "enableStopDropAndRollOnColonists", (M0) 1, false);
      // ISSUE: cast to a reference type
      Scribe_Values.Look<bool>((M0&) ref this.enableStopDropAndRollOnNonColonists, "enableStopDropAndRollOnNonColonists", (M0) 0, false);
      // ISSUE: cast to a reference type
      Scribe_Values.Look<bool>((M0&) ref this.enableStopDropAndRollOnAnimals, "enableStopDropAndRollOnAnimals", (M0) 0, false);
      // ISSUE: cast to a reference type
      Scribe_Values.Look<bool>((M0&) ref this.enableCustomFireAttachOnColonists, "enableCustomFireAttachOnColonists", (M0) 1, false);
      // ISSUE: cast to a reference type
      Scribe_Values.Look<bool>((M0&) ref this.enableCustomFireAttachOnNonColonists, "enableCustomFireAttachOnNonColonists", (M0) 1, false);
      // ISSUE: cast to a reference type
      Scribe_Values.Look<bool>((M0&) ref this.enableCustomFireAttachOnAnimals, "enableCustomFireAttachOnAnimals", (M0) 1, false);
    }

    public void DoSettingsWindowContents(Rect inRect)
    {
      Listing_Standard listingStandard = new Listing_Standard();
      ((Listing) listingStandard).Begin(inRect);
      listingStandard.Label(TaggedString.op_Addition(TaggedString.op_Addition(Translator.Translate("SDR.IRS"), ": "), GenText.ToStringDecimalIfSmall(this.IRS)), -1f, (string) null);
      this.IRS = listingStandard.Slider(this.IRS, 0.0f, 10f);
      listingStandard.Label(TaggedString.op_Addition(TaggedString.op_Addition(Translator.Translate("SDR.MHA"), ": "), GenText.ToStringDecimalIfSmall(this.MHA)), -1f, (string) null);
      this.MHA = listingStandard.Slider(this.MHA, 0.0f, 2f);
      listingStandard.CheckboxLabeled(TaggedString.op_Implicit(Translator.Translate("SDR.EnableOnColonists")), ref this.enableStopDropAndRollOnColonists, (string) null);
      listingStandard.CheckboxLabeled(TaggedString.op_Implicit(Translator.Translate("SDR.EnableOnNonColonists")), ref this.enableStopDropAndRollOnNonColonists, (string) null);
      listingStandard.CheckboxLabeled(TaggedString.op_Implicit(Translator.Translate("SDR.EnableOnAnimals")), ref this.enableStopDropAndRollOnAnimals, (string) null);
      listingStandard.CheckboxLabeled(TaggedString.op_Implicit(Translator.Translate("SDR.EnableOnColonists2")), ref this.enableCustomFireAttachOnColonists, (string) null);
      listingStandard.CheckboxLabeled(TaggedString.op_Implicit(Translator.Translate("SDR.EnableOnNonColonists2")), ref this.enableCustomFireAttachOnNonColonists, (string) null);
      listingStandard.CheckboxLabeled(TaggedString.op_Implicit(Translator.Translate("SDR.EnableOnAnimals2")), ref this.enableCustomFireAttachOnAnimals, (string) null);
      if (listingStandard.ButtonText(TaggedString.op_Implicit(Translator.Translate("SDR.Reset")), (string) null))
      {
        this.IRS = 4f;
        this.MHA = 0.25f;
        this.enableStopDropAndRollOnColonists = true;
        this.enableStopDropAndRollOnNonColonists = false;
        this.enableStopDropAndRollOnAnimals = false;
        this.enableCustomFireAttachOnColonists = true;
        this.enableCustomFireAttachOnNonColonists = true;
        this.enableCustomFireAttachOnAnimals = true;
      }
      ((Listing) listingStandard).End();
      this.Write();
    }

    public StopDropAndRollSettings()
    {
      base.\u002Ector();
    }
  }
}
