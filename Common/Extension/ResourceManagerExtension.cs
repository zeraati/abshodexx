using System.Resources;
using System.Collections;
using System.Globalization;

namespace Common.Extension;

public static class ResourceManagerExtension
{
    public static string? ResourceKey(this ResourceManager manager, string? value)
    {
        if(value.Empty()) return null;

        var resourceSet = manager.GetResourceSet(new CultureInfo("fa-IR"), true, true);
        if (resourceSet == null) return null;

        foreach (DictionaryEntry item in resourceSet)
        {
            if (item.Value?.ToString() == value) return item.Key.ToString();
        }

        return null;
    }
}
