using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SaiyanTails.Utils;

using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SaiyanTails.Draw;

public sealed class SaiyanTail : PlayerDrawLayer {
    private static readonly IncrementingConcurrentDictionary<int> Tick = new();

    public override bool IsHeadLayer => false;

    public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.Torso);

    protected override void Draw(ref PlayerDrawSet drawInfo) {
        Player drawPlayer = drawInfo.drawPlayer;

        long state = Tick[drawPlayer.whoAmI] / 20L % 20L;
        long y = 56L * state;

        var color = GetColor(drawPlayer);
        const string asset = "SaiyanTails/Draw/tail";

        DrawData data = new(ModContent.Request<Texture2D>(asset, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value,
            new Vector2((int)(drawInfo.Position.X - Main.screenPosition.X - drawPlayer.bodyFrame.Width / 4 + drawPlayer.width),
            drawInfo.Position.Y - Main.screenPosition.Y + drawPlayer.height / 5 + 2f) + drawPlayer.headPosition + drawInfo.hairOffset + (drawPlayer.ItemAnimationActive ? new(0f, 2f) : new(0f, 0f)),
            new(0, (int)y, 40, 56), color, drawPlayer.headRotation, drawInfo.headVect, 1f, drawInfo.playerEffect, 0);

        drawInfo.DrawDataCache.Add(data);
    }

    private static Type TransformationHelper => m_TransformationHelper ??= SaiyanTails.DBT.DefinedTypes.FirstOrDefault(a => a.Name.Equals("TransformationHelper"));
    private static Type DbzcalamityPlayer => m_DbzcalamityPlayer ??= SaiyanTails.DBCA.DefinedTypes.FirstOrDefault(a => a.Name.Equals("dbzcalamityPlayer"));
    private static Type OPlayer => m_OPlayer ??= SaiyanTails.Oozaru.DefinedTypes.FirstOrDefault(a => a.Name.Equals("OPlayer"));

    internal static Color GetColor(Player player) {
        if (SaiyanTails.DBT != null && TransformationHelper != null && TransformationHelper != default) {
            dynamic currentForm = TransformationHelper?.Invoke<dynamic>("GetCurrentTransformation", new object[] { player, false, false });

            if (currentForm != null) {
                int? menuId = (int?)currentForm?.menuId;

                if (menuId.HasValue && m_colors.ContainsKey(menuId.Value))
                    return m_colors[menuId.Value];

                return currentForm.transformationTextColor;
            }
        }

        if (SaiyanTails.DBCA != null && DbzcalamityPlayer != null && DbzcalamityPlayer != default) {
            dynamic dbzcalamityPlayerI = DbzcalamityPlayer?.Invoke<dynamic>("ModPlayer", new object[] { player });

            if (dbzcalamityPlayerI != null) {
                if (DbzcalamityPlayer.GetField("uiActive").GetValue(dbzcalamityPlayerI))
                    return IUICol;
                if (DbzcalamityPlayer.GetField("puiActive").GetValue(dbzcalamityPlayerI))
                    return MUICol;
                if (DbzcalamityPlayer.GetField("ueActive").GetValue(dbzcalamityPlayerI))
                    return UECol;
            }
        }

        if (SaiyanTails.Oozaru != null && OPlayer != null & OPlayer != default) {
            var modPlayers = player.ModPlayers;

            foreach (var mPlayer in modPlayers) {
                if (mPlayer.Name.Equals("OPlayer")) {
                    if ((bool)OPlayer.GetField("SSJ4Active").GetValue(mPlayer)) {
                        return SSJ4Col;
                    }
                    if ((bool)OPlayer.GetField("SSJ4FPActive").GetValue(mPlayer)) {
                        return SSJ4FPCol;
                    }
                    if ((bool)OPlayer.GetField("SSJ4LBActive").GetValue(mPlayer)) {
                        return SSJ4LBCol;
                    }
                }
            }
        }


        var color = player.GetHairColor(true);
        return new Color(color.R - 5, color.G - 5, color.B - 5);
    }

    private static Type m_TransformationHelper;
    private static Type m_DbzcalamityPlayer;
    private static Type m_OPlayer;

    private static readonly Dictionary<int, Color> m_colors = new() {
        { 1, new(255, 240, 40) },
        { 2, new(255, 200, 0) },
        { 3, new(255, 220, 60) },
        { 4, new(0, 255, 30) },
        { 6, new(0, 220, 16) },
        { 7, new(40, 255, 20) },
        { 5, new(255, 0, 44) },
        { 8, new(0, 150, 255) },
        { 9, new(255, 160, 255) },
    };

    private static readonly Color IUICol = new(0, 0, 20);
    private static readonly Color MUICol = new(236, 236, 248);
    private static readonly Color UECol = new(156, 0, 255);

    private static readonly Color SSJ4Col = new(0, 0, 0);
    private static readonly Color SSJ4FPCol = new(25, 25, 25);
    private static readonly Color SSJ4LBCol = new(255, 50, 50);
}