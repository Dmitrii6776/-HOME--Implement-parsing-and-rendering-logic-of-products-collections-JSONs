using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UIElements;


public class JsonReader : MonoBehaviour
{
    [SerializeField] private TextAsset _jsonClothes;

    [SerializeField] private TextAsset _jsonCatalog;
    [SerializeField] private ItemManager _itemManager;


    void Awake()
    {
        var jsonClothesString = 
            File.ReadAllText("/Users/dmitrijposin/Desktop/projects/My project/Assets/JsonData/clothes_f_v1.4.json");
        var clothesJson = JSON.Parse(jsonClothesString);
        _itemManager.ClothesJson = clothesJson;
        var jsonColString =
            File.ReadAllText("/Users/dmitrijposin/Desktop/projects/My project/Assets/JsonData/sample_catalog.json");
        var colJson = JSON.Parse(jsonColString);
        _itemManager.CollectionsJson = colJson;

    }

}
