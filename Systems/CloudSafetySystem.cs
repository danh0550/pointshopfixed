using Terraria;
using Terraria.ModLoader;

namespace PointShop.Systems;

public class CloudSafetySystem : ModSystem
{
    public override void Load()
    {
        On.Terraria.Cloud.resetClouds += Cloud_resetClouds;
        On.Terraria.Cloud.addCloud += Cloud_addCloud;
    }

    public override void Unload()
    {
        On.Terraria.Cloud.resetClouds -= Cloud_resetClouds;
        On.Terraria.Cloud.addCloud -= Cloud_addCloud;
    }

    private static void EnsureValidCloudLimit()
    {
        if (Main.cloudLimit < 1)
        {
            Main.cloudLimit = 1;
        }
    }

    private void Cloud_resetClouds(On.Terraria.Cloud.orig_resetClouds orig)
    {
        EnsureValidCloudLimit();
        orig();
    }

    private void Cloud_addCloud(On.Terraria.Cloud.orig_addCloud orig)
    {
        EnsureValidCloudLimit();
        orig();
    }
}
