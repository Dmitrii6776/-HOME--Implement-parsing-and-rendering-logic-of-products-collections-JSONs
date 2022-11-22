using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ItemManager : MonoBehaviour
{
    public JSONNode ClothesJson;
    public JSONNode CollectionsJson;
    [SerializeField] private ItemComponent _itemRecentsPrefab;
    [SerializeField] private GameObject _itemRecentsParent;
    [SerializeField] private ItemCollectionsComponent _itemCollectionsPrefab;
    [SerializeField] private GameObject _itemCollectionsParent;
    private ConcurrentBag<ItemCollectionsComponent> _collectionItems = new ConcurrentBag<ItemCollectionsComponent>();

    private void Start()
    {
        FillRecentsWindow();
        FillCollectionsWindow();
        
    }

    private IEnumerator DefRemoteImage(string url, Image currentImage)
    {
        var request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }

        var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        var sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        currentImage.sprite = sprite;
    }

    private void FillRecentsWindow()
    {
        foreach (var v in ClothesJson["Clothes"])
        {
            var newItem = Instantiate(_itemRecentsPrefab.transform, _itemRecentsParent.transform);
            var item = newItem.GetComponent<ItemComponent>();
            item.Name.text = StringChanger(v.Value["name"]);
            item.Subcaregory.text = StringChanger(v.Value["subcategory"]);
            item.DateCreateFrom.text = StringChanger(v.Value["createdAt"]);
            StartCoroutine(DefRemoteImage(v.Value["images"][0]["url"], item.RemoteImage));
        } 
    }

    private string StringChanger(string str)
    {
        var changedStr = str?.ToUpper();
        return changedStr?.Replace("_", " ");
     
    }

    private void FillCollectionsWindow()
    {
        foreach (var v in CollectionsJson)
        {
            
                var tempCount = 0;
                foreach (var i in _collectionItems)

                {
                    if (v.Value["name"] == i.CollectionName)
                    {
                        continue;
                    }
                }
                CreateNewCollection(v);

        }

        foreach (var col in _collectionItems)
        {
            foreach (var i in CollectionsJson)
            {
                if (col.CollectionName == i.Value["name"] && col.ItemsInCollectionCount < 5)
                {
                    foreach (var v in i.Value["products"][0]["images"])
                    {
                        var newItem = Instantiate(col.SubItemPrefab.transform, col.SubItemParent.transform)
                            .GetComponent<Image>();
                        Debug.Log(i.Value["products"][0]["images"][0]["url"]);
                        StartCoroutine(DefRemoteImage(v.Value["url"], newItem));
                        col.ItemsInCollectionCount += 1;
                    }
                    
                }
            }
        }
    }

    private void CreateNewCollection(KeyValuePair<string, JSONNode> v)
    {
        var newCollection =
            Instantiate(_itemCollectionsPrefab.transform, _itemCollectionsParent.transform)
                .GetComponent<ItemCollectionsComponent>();
        newCollection.CollectionName.text = v.Value["name"];
        _collectionItems.Add(newCollection);
    }
    
}
