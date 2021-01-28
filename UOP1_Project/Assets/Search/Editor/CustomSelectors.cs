using UnityEngine;
using UnityEditor.Search;

static class CustomSelectors
{
	[SearchExpressionSelector("^path", provider: "scene")]
	static object SelectPath(SearchExpressionSelectorArgs args)
	{
		var go = args.current.ToObject<GameObject>();
		if (!go)
			return null;

		return SearchUtils.GetHierarchyPath(go, false);
	}

	[SearchExpressionSelector("^vertices$", provider: "scene")]
    static object SelectVertices(SearchExpressionSelectorArgs args)
	{
		var go = args.current.ToObject<GameObject>();
		if (!go)
			return null;

		var meshFilter = go.GetComponent<MeshFilter>();
		if (!meshFilter || !meshFilter.sharedMesh)
			return null;

		return meshFilter.sharedMesh.vertexCount;
	}
}
