using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect: MonoBehaviour {

    private int selectedIndex;
    private int selectedWomenIndex = 6;
    private int selectedGenderIndex;
    private int selectedSexIndex;


    [Header ("List of Sprites")]
    [SerializeField] private List<charSelectObject> charList = new List<charSelectObject>();

    [Header ("List of Genders")]
    [SerializeField] private List <genderSelectObject> genderList = new List <genderSelectObject> ();

    [Header ("List of Sexuality")]
    [SerializeField] private List <sexualitySelectObject> sexList = new List <sexualitySelectObject> ();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI race;
    [SerializeField] private TextMeshProUGUI gender;
    [SerializeField] private TextMeshProUGUI sexuality;
    [SerializeField] private Image characterSprite;
    
    private void Start(){
        // selectedIndex = PlayerPrefs.GetInt("CharacterSelected");
        UpdateCharSelectUI();
    }

    public void spriteLeftArrow(){
        selectedIndex--;
        selectedWomenIndex--;

        // reset sprite to male end
        if(selectedIndex < 0 && selectedGenderIndex == 0){
            selectedIndex = 5;
        }
        // reset sprite to female end
        else if(selectedWomenIndex < 6 && selectedGenderIndex == 1){
            selectedWomenIndex = charList.Count - 1;
        }
        // reset sprites
        else if(selectedIndex < 0 && selectedGenderIndex == 2){
            selectedIndex = charList.Count - 1;
        }
        UpdateCharSelectUI();
    }

    public void spriteRightArrow(){
        selectedIndex++;
        selectedWomenIndex++;
        
        // reset sprite to male start
        if(selectedIndex == 5 && selectedGenderIndex == 0){
            selectedIndex = 0;
        }
        // reset sprite to female start
        else if(selectedWomenIndex == charList.Count && selectedGenderIndex == 1){
            selectedWomenIndex = 6;
        }
        // reset sprite
        else if(selectedIndex == charList.Count && selectedGenderIndex == 2){
            selectedIndex = 0;
        }
        UpdateCharSelectUI();
    }

    public void genderLeftArrow(){
        selectedGenderIndex--;
        if(selectedGenderIndex < 0){
            selectedGenderIndex = genderList.Count - 1;
        }
        UpdateCharSelectUI();
    }

    public void genderRightArrow(){
        selectedGenderIndex++;
        if(selectedGenderIndex == genderList.Count){
            selectedGenderIndex = 0;
        }
        UpdateCharSelectUI();
    }

        public void sexLeftArrow(){
        selectedSexIndex--;
        if(selectedSexIndex < 0){
            selectedSexIndex = sexList.Count -1;
        }
        UpdateCharSelectUI();
    }
    
    public void sexRightArrow(){
        selectedSexIndex++;
        if(selectedSexIndex == sexList.Count){
            selectedSexIndex = 0;
        }
        UpdateCharSelectUI();
    }

    private void UpdateCharSelectUI(){
        // Update Sprite, Race, Gender, and Sex

        gender.text = genderList[selectedGenderIndex].gender;
        sexuality.text = sexList[selectedSexIndex].sexuality;

        if (selectedGenderIndex == 0){
                characterSprite.sprite = charList[selectedIndex].charSprite;
                race.text = charList[selectedIndex].race;
        }
        else if (selectedGenderIndex == 1){
                characterSprite.sprite = charList[selectedWomenIndex].charSprite;
                race.text = charList[selectedWomenIndex].race;
        }
        else {
            characterSprite.sprite = charList[selectedIndex].charSprite;
            race.text = charList[selectedIndex].race;
        }
    }

    // switch to game scene
    public void startButton(){
        // PlayerPrefs.SetInt("CharacterSelected", selectedIndex);
        SceneManager.LoadScene("World");
    }

    [System.Serializable]
    public class charSelectObject{
        public Sprite charSprite;
        public string race;
    }

    [System.Serializable]
    public class genderSelectObject{
        public string gender;
    }

      [System.Serializable]
    public class sexualitySelectObject{
        public string sexuality;
    }
}