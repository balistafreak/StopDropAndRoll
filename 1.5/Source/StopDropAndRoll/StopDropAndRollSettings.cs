using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace StopDropAndRoll
{
    public class StopDropAndRollSettings : ModSettings
    {
        public float IRS = 4f;
        public float MHA = 0.25f;
        public bool enableStopDropAndRollOnColonists = true;
        public bool enableStopDropAndRollOnNonColonists = false;
        public bool enableStopDropAndRollOnAnimals = false;
        public bool enableCustomFireAttachOnColonists = true;
        public bool enableCustomFireAttachOnNonColonists = true;
        public bool enableCustomFireAttachOnAnimals = true;
        private string buff1 = "";
        private string buff2 = "";
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref IRS, "IRS", 4f);
            Scribe_Values.Look(ref MHA, "MHA", 0.25f);
            Scribe_Values.Look(ref enableStopDropAndRollOnColonists, "enableStopDropAndRollOnColonists", true);
            Scribe_Values.Look(ref enableStopDropAndRollOnNonColonists, "enableStopDropAndRollOnNonColonists", false);
            Scribe_Values.Look(ref enableStopDropAndRollOnAnimals, "enableStopDropAndRollOnAnimals", false);

            Scribe_Values.Look(ref enableCustomFireAttachOnColonists, "enableCustomFireAttachOnColonists", true);
            Scribe_Values.Look(ref enableCustomFireAttachOnNonColonists, "enableCustomFireAttachOnNonColonists", true);
            Scribe_Values.Look(ref enableCustomFireAttachOnAnimals, "enableCustomFireAttachOnAnimals", true);
        }
        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("SDR.IRS".Translate() + ": " + IRS.ToStringDecimalIfSmall());
            IRS = listingStandard.Slider(IRS, 0f, 10f); 
            listingStandard.Label("SDR.MHA".Translate() + ": " + MHA.ToStringDecimalIfSmall());
            MHA = listingStandard.Slider(MHA, 0f, 2f);
            listingStandard.CheckboxLabeled("SDR.EnableOnColonists".Translate(), ref enableStopDropAndRollOnColonists);
            listingStandard.CheckboxLabeled("SDR.EnableOnNonColonists".Translate(), ref enableStopDropAndRollOnNonColonists);
            listingStandard.CheckboxLabeled("SDR.EnableOnAnimals".Translate(), ref enableStopDropAndRollOnAnimals);

            listingStandard.CheckboxLabeled("SDR.EnableOnColonists2".Translate(), ref enableCustomFireAttachOnColonists);
            listingStandard.CheckboxLabeled("SDR.EnableOnNonColonists2".Translate(), ref enableCustomFireAttachOnNonColonists);
            listingStandard.CheckboxLabeled("SDR.EnableOnAnimals2".Translate(), ref enableCustomFireAttachOnAnimals);

            if (listingStandard.ButtonText("SDR.Reset".Translate()))
            {
                IRS = 4f;
                MHA = 0.25f;
                enableStopDropAndRollOnColonists = true;
                enableStopDropAndRollOnNonColonists = false;
                enableStopDropAndRollOnAnimals = false;
                enableCustomFireAttachOnColonists = true;
                enableCustomFireAttachOnNonColonists = true;
                enableCustomFireAttachOnAnimals = true;
            }
            listingStandard.End();
            base.Write();
        }
    }
}
