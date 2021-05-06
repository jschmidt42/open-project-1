using UnityEngine;
using UnityEngine.Search;

public class HouseProperties : MonoBehaviour
{
	const string providers = "expression,scene,asset";
    const SearchViewFlags flags = SearchViewFlags.Centered | SearchViewFlags.GridView | SearchViewFlags.HideSearchBar;

    [SearchContext("expression: h: ref=select{p:bighouse, @path}", providers, flags)]
    public GameObject largeHouse;

    [SearchContext("expression: h: ref=select{p:smallhouse -door, @path}", providers, flags)]
    public GameObject smallHouse;
}
