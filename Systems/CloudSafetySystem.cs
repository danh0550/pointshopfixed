using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace PointShop.Systems;

public class CloudSafetySystem : ModSystem
{
    private static FieldInfo? CloudLimitField;

    public override void PostUpdateEverything()
    {
        EnsureValidCloudLimit();
    }

    public override void PreDrawTiles()
    {
        EnsureValidCloudLimit();
    }

    private static void EnsureValidCloudLimit()
    {
        CloudLimitField ??= FindCloudLimitField();
        if (CloudLimitField == null)
        {
            return;
        }

        var currentValue = (int)CloudLimitField.GetValue(null)!;
        if (currentValue < 1)
        {
            CloudLimitField.SetValue(null, 1);
        }
    }

    private static FieldInfo? FindCloudLimitField()
    {
        return FindStaticIntField(typeof(Main), "cloudLimit")
            ?? FindStaticIntField(typeof(Main), "maxClouds")
            ?? FindStaticIntField(typeof(Cloud), "cloudLimit")
            ?? FindStaticIntField(typeof(Cloud), "maxClouds");
    }

    private static FieldInfo? FindStaticIntField(Type type, string fieldName)
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
        var field = type.GetField(fieldName, flags);
        return field?.FieldType == typeof(int) ? field : null;
    }
}
