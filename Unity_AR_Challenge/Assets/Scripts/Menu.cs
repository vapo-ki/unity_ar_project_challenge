using PolyToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public List<ModelPreview> modelPreviewList = new List<ModelPreview>();
    public TMP_InputField searchBar;

    public TextMeshProUGUI pageText;
    public GameObject filterMenu;
    public Toggle curatedToggle;
    public Dropdown maxComplexityDropdown;
    public Dropdown formatFilterDropdown;
    public Dropdown categoryDropdown;
    public Dropdown sortByDropdown;
    public TextMeshProUGUI noResultsText;

    private PolyListAssetsRequest req;
    private List<PolyAsset> searchResults;
    private int page;

    private void Start()
    {
        //Initialize
        page = 0;
        
        req = new PolyListAssetsRequest();

        //Get Featured Models 
        PolyApi.ListAssets(PolyListAssetsRequest.Featured(), LoadModels);
    }

    private void Search(string searchString)
    {
        req.keywords = searchString;
        print(req);
        PolyApi.ListAssets(req, LoadModels);
    }

    private void LoadModels(PolyStatusOr<PolyListAssetsResult> result)
    {
        if (!result.Ok)
        {
            print("Search Error.");
            Debug.Log(result.Status.errorMessage);
            return;
        }

        searchResults = result.Value.assets;
        Refresh();
    }

    private void Refresh()
    {
        var count = 1;

        foreach (ModelPreview modelPreview in modelPreviewList)
        {
            if ((count + (page * 6)) <= searchResults.Count-1)
            {
                modelPreview.SetPolyModel(searchResults[count + (page * 6)]);
                count++;
            } else
            {
                modelPreview.SetEmpty();
}
        }

        if (searchResults.Count == 0)
        {
            noResultsText.gameObject.SetActive(true);
        } else
        {
            noResultsText.gameObject.SetActive(false);
        }
    }

    public void _OnNextPageButton()
    {
        if (((page+1) * 6) <= (searchResults.Count))
        {
            page++;
            pageText.SetText((page + 1).ToString());
            Refresh();
        }
        
    }

    public void _OnPreviousPageButton()
    {
        if (page > 0)
        {
            page--;
            pageText.SetText((page + 1).ToString());
            Refresh();
        }
    }

    public void _OnHitEnterOrOnHitSearchButton()
    {
        Search(searchBar.text);
    }

    public void _OnFilterButton()
    {
        filterMenu.SetActive(true);
    }

    //Filters
    public void _OnConfirm()
    {
        //Curated
        req.curated = curatedToggle.isOn;
        print("Curated turned: " + curatedToggle.isOn);

        //Max Complexity
        switch (maxComplexityDropdown.value)
        {
            case 0:
                req.maxComplexity = PolyMaxComplexityFilter.UNSPECIFIED; break;
            case 1:
                req.maxComplexity = PolyMaxComplexityFilter.SIMPLE; break;
            case 2:
                req.maxComplexity = PolyMaxComplexityFilter.MEDIUM; break;
            case 3:
                req.maxComplexity = PolyMaxComplexityFilter.COMPLEX; break;
        }

        //Format Filter
        switch (formatFilterDropdown.value)
        {
            case 0:
                req.formatFilter = null; break;
            case 1:
                req.formatFilter = PolyFormatFilter.BLOCKS; break;
            case 2:
                req.formatFilter = PolyFormatFilter.FBX; break;
            case 3:
                req.formatFilter = PolyFormatFilter.GLTF; break;
            case 4:
                req.formatFilter = PolyFormatFilter.GLTF_2; break;
            case 5:
                req.formatFilter = PolyFormatFilter.OBJ; break;
            case 6:
                req.formatFilter = PolyFormatFilter.TILT; break;
        }

        //Category
        switch (categoryDropdown.value)
        {
            case 0:
                req.category = PolyCategory.UNSPECIFIED; break;
            case 1:
                req.category = PolyCategory.ANIMALS; break;
            case 2:
                req.category = PolyCategory.ARCHITECTURE; break;
            case 3:
                req.category = PolyCategory.ART; break;
            case 4:
                req.category = PolyCategory.FOOD; break;
            case 5:
                req.category = PolyCategory.NATURE; break;
            case 7:
                req.category = PolyCategory.OBJECTS; break;
            case 8:
                req.category = PolyCategory.PEOPLE; break;
            case 9:
                req.category = PolyCategory.PLACES; break;
            case 10:
                req.category = PolyCategory.TECH; break;
            case 11:
                req.category = PolyCategory.TRANSPORT; break;
        }

        //Order By
        switch (sortByDropdown.value)
        {
            case 0:
                req.orderBy = PolyOrderBy.BEST; break;
            case 1:
                req.orderBy = PolyOrderBy.NEWEST; break;
            case 2:
                req.orderBy = PolyOrderBy.OLDEST; break;
        }
        filterMenu.SetActive(false);
    }
}
