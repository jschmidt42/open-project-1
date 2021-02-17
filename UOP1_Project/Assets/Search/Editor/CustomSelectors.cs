using UnityEngine;
using UnityEditor.Search;
using UnityEditor;

static class CustomSelectors
{
	[SearchSelector("^vertices$", provider: "scene")]
    static object SelectVertices(SearchSelectorArgs args)
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
