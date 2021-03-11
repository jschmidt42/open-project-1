using UnityEngine;
using UnityEditor.Search;
using UnityEditor;
using System.Linq;

static class SearchPrefabWorkflow
{
	const string providerId = "scene";

	[SearchSelector("~pc", provider: providerId)]
	static object GetModifiedPropertyCount(SearchSelectorArgs args)
	{
		var go = args.current.ToObject<GameObject>();
		if (!go)
			return null;
		var overrides = PrefabUtility.GetPropertyModifications(go);
		if (overrides == null)
			return null;
		var importantOverrides = 0;
		foreach (var o in overrides)
		{
			if (!IsImportantOverride(o))
				continue;
			importantOverrides++;
		}
		return importantOverrides;
	}

	static PropertyModification GetPropertyModifition(SearchSelectorArgs args)
	{
		if (!(args["override_index"] is string overrideIndexString))
			return null;

		if (!Utils.TryParse(overrideIndexString, out int overrideIndex))
			return null;

		var go = args.current.ToObject<GameObject>();
		if (!go)
			return null;

		var overrides = PrefabUtility.GetPropertyModifications(go);
		if (overrides == null)
			return null;
		overrides = overrides.Where(IsImportantOverride).ToArray();
		if (overrideIndex < 0 || overrideIndex >= overrides.Length)
			return null;

		return overrides[overrideIndex];
	}

	[SearchSelector(@"~pn\[(?<override_index>\d+)\]", provider: providerId)]
	static object GetPrefabOverrideNameAtIndex(SearchSelectorArgs args)
	{
		var pm = GetPropertyModifition(args);
		if (pm == null)
			return null;
		return pm.propertyPath;
	}

	[SearchSelector(@"~pv\[(?<override_index>\d+)\]", provider: providerId)]
	static object GetPrefabOverrideValueAtIndex(SearchSelectorArgs args)
	{
		var pm = GetPropertyModifition(args);
		if (pm == null)
			return null;
		return !string.IsNullOrEmpty(pm.value) ? (object)pm.value : pm.objectReference;
	}

	static bool IsImportantOverride(PropertyModification pm)
	{
		if (string.Equals(pm.propertyPath, "m_Name", System.StringComparison.Ordinal))
			return false;
		if (pm.target is Transform)
			return false;
		return true;
	}
}
