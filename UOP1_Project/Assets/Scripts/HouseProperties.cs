using UnityEngine;
using UnityEngine.Search;

public class HouseProperties : MonoBehaviour
{
	const string providers = "expression,scene,asset";
    const SearchContextFlags flags = SearchContextFlags.Centered | SearchContextFlags.GridView | SearchContextFlags.HideSearchBar;

    [SearchContext("expression: h: ref=select{p:bighouse, @path}", providers, flags)]
    public GameObject largeHouse;

    [SearchContext("expression: h: ref=select{p:smallhouse -door, @path}", providers, flags)]
    public GameObject smallHouse;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
