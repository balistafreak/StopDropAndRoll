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
    public class StopDropAndRollMod : Mod
    {
        public static StopDropAndRollSettings settings;
        public StopDropAndRollMod(ModContentPack pack) : base(pack)
        {
            settings = GetSettings<StopDropAndRollSettings>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Stop Drop And Roll";
        }
    }
}
