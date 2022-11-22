using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[System.Serializable]
public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _recentsPanel;
    [SerializeField] private GameObject _collectionsPanel;
    [SerializeField] private GameObject _recentsIndicator;
    [SerializeField] private GameObject _collectionsIndicator;


    public void OnRecentsButtonClick()
    {
        _recentsIndicator.SetActive(true);
        _recentsPanel.SetActive(true);
   
        _collectionsIndicator.SetActive(false);
        _collectionsPanel.SetActive(false);
        
    }
    public void OnCollectionsButtonClick()
    {
        _recentsIndicator.SetActive(false);
        _recentsPanel.SetActive(false);
   
        _collectionsIndicator.SetActive(true);
        _collectionsPanel.SetActive(true);
        
    }
}
