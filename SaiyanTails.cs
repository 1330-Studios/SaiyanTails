using System;
using System.Reflection;

using log4net;

using Microsoft.Xna.Framework.Input;

using Terraria.ModLoader;

namespace SaiyanTails; 

public sealed class SaiyanTails : Mod {
    public static Assembly DBT;
    public static Assembly DBCA;

    public override void PostSetupContent() {
        DBT = ModLoader.TryGetMod("DBZMODPORT", out var dbzmod) ? dbzmod.Code : null;
        DBCA = ModLoader.TryGetMod("dbzcalamity", out var dbcamod) ? dbcamod.Code : null;
    }
}
