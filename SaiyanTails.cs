using System.Reflection;

using Terraria.ModLoader;

namespace SaiyanTails;

public sealed class SaiyanTails : Mod {
    public static Assembly DBT;
    public static Assembly DBCA;
    public static Assembly Oozaru;

    public override void PostSetupContent() {
        DBT = ModLoader.TryGetMod("DBZMODPORT", out var dbzmod) ? dbzmod.Code : null;
        DBCA = ModLoader.TryGetMod("dbzcalamity", out var dbcamod) ? dbcamod.Code : null;
        Oozaru = ModLoader.TryGetMod("oozaru", out var oozarumod) ? oozarumod.Code : null;
    }
}
