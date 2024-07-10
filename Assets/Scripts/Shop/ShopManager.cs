using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;


//update buttons when buy or in use
//create database
public class ShopManager : MonoBehaviour
{   
    [SerializeField] private ShopDataManager ShopDM;
    [Header ("Scrollers & Buttons")]
    [SerializeField] private GameObject BulletScroller;
    [SerializeField] private GameObject CharScroller;
    [SerializeField] private Button BulletBtn;
    [SerializeField] private Button CharBtn;

    [Header ("Token Text")]
    [SerializeField] private Text TokenText;

    [Header ("Item List")]
    [SerializeField] private GameObject[] Bullets;
    [SerializeField] private Bullet[] bullets;

    [SerializeField] private GameObject[] Characters;
    [SerializeField] private Character[] characters;

    [Header ("Error Object")]
    [SerializeField] private GameObject messageTextObj;
    [SerializeField] private float speed;
    [SerializeField] private RectTransform destination;
    
    [SerializeField] private float smoothTime;

    [SerializeField] private AudioClip SelectSound;
        

    private bool message; 
    private RectTransform messageTextObjRT;
    private Vector3 initPos;
    private Vector3 velocity = Vector3.zero;
    private Text messageText;

    private ShopData data;

    private int SelectedCharacter; 
    private int SelectedBullet;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
        PlayerPrefs.SetInt("Tokens", 0);
    } 

    // Start is called before the first frame update
    void Awake()
    {   
        

        UpdateShop();

        BulletScroller.SetActive(true);
        CharScroller.SetActive(false);
        BulletBtn.interactable = false;
        CharBtn.interactable = true;

        
        messageTextObj.SetActive(false);
        message = false;
        messageTextObjRT = messageTextObj.GetComponent<RectTransform>();
        messageText = messageTextObj.GetComponent<Text>();
        initPos = messageTextObjRT.localPosition;
    }

    private void UpdateShop()
     {
        data = ShopDM.GetData();

        SelectedCharacter = data.SelectedCharacter;
        SelectedBullet = data.SelectedBullet;

        characters[0].isUnlock = data.Character1;
        characters[1].isUnlock = data.Character2;
        characters[2].isUnlock = data.Character3;
        characters[3].isUnlock = data.Character4;
        characters[4].isUnlock = data.Character5;

        bullets[0].isUnlock = data.Bullet1;
        bullets[1].isUnlock = data.Bullet2;
        bullets[2].isUnlock = data.Bullet3;
        bullets[3].isUnlock = data.Bullet4;
        bullets[4].isUnlock = data.Bullet5;

        UpdateBulletShop();

        UpdateCharacterShop();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the token value to the player database and pass it tokens var
        TokenText.text = Convert.ToString(PlayerPrefs.GetInt("Tokens"));

        if(message){
            //messageTextObjRT.localPosition = new Vector3(destination.localPosition.x, destination.localPosition.y * speed * , destination.localPosition.z);
            messageTextObjRT.localPosition = Vector3.SmoothDamp(messageTextObjRT.localPosition, destination.localPosition, ref velocity, smoothTime, speed, Time.deltaTime);
            
            //error = false;
            if(messageTextObjRT.localPosition != destination.localPosition){
                message = true;
                
            }else{
                message = false;
                messageTextObj.SetActive(false);
                messageTextObjRT.localPosition = new Vector3(initPos.x, initPos.y, initPos.z);
            }
        }
    }

    private void UpdateCharacterShop() {
        for(int i = 0; i < Characters.Length; i++) {
            Characters[i].transform.Find("Item").gameObject.transform.GetChild(0).GetComponent<Text>().text = characters[i].name;

            if(characters[i].isUnlock == true && i != SelectedCharacter){
                Characters[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text = "USE";

                if(Characters[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text == "USE"){
                    Characters[i].transform.Find("Buybtn").GetComponent<Button>().interactable = true;
                }
            }
            if(characters[i].isUnlock == true && i == SelectedCharacter){
                Characters[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text = "IN USE";
                Characters[i].transform.Find("Buybtn").GetComponent<Button>().interactable = false;

                if(Characters[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text == "IN USE"){
                    Characters[i].transform.Find("Buybtn").GetComponent<Button>().interactable = false;
                }
            }
            if(characters[i].isUnlock == false){
                Characters[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text = Convert.ToString(characters[i].price);
            }
        }
    }

    private void UpdateBulletShop() {
        for(int i = 0; i < Bullets.Length; i++) {
            Bullets[i].transform.Find("Item").gameObject.transform.GetChild(0).GetComponent<Text>().text = bullets[i].name;

            if(bullets[i].isUnlock == true && i != SelectedBullet){
                Bullets[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text = "USE";

                if(Bullets[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text == "USE"){
                    Bullets[i].transform.Find("Buybtn").GetComponent<Button>().interactable = true;
                }
            }
            if(bullets[i].isUnlock == true && i == SelectedBullet){
                Bullets[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text = "IN USE";
                Bullets[i].transform.Find("Buybtn").GetComponent<Button>().interactable = false;

                if(Bullets[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text == "IN USE"){
                    Bullets[i].transform.Find("Buybtn").GetComponent<Button>().interactable = false;
                }
            }
            if(bullets[i].isUnlock == false){
                Bullets[i].transform.Find("Buybtn").gameObject.transform.GetChild(0).GetComponent<Text>().text = Convert.ToString(bullets[i].price);
            }
        }
    }

    public void BuyBullet(int ItemIndex) {
        SoundManager.instance.PlaySound(SelectSound);

        if(bullets[ItemIndex].isUnlock == false){

            GameObject btn = Bullets[ItemIndex].transform.Find("Buybtn").gameObject;

            int Price = Convert.ToInt32(btn.transform.GetChild(0).gameObject.GetComponent<Text>().text);

            if(PlayerPrefs.GetInt("Tokens") >= Price){
                PlayerPrefs.SetInt("Tokens", PlayerPrefs.GetInt("Tokens") - Price);

                ShopDM.UnlockBullet(ItemIndex);
                UpdateShop();

                messageText.text = "You Successfully Bought A Weapon!";
                SendMessage();
            }else{
                messageText.text = "Not Enough Coins!";
                SendMessage();
            }
        }else if(bullets[ItemIndex].isUnlock == true && ItemIndex != SelectedBullet){
            ShopDM.UpdateSelectedBullet(ItemIndex);
            UpdateShop();
        }
            
    }

    public void BuyChar(int ItemIndex) {
        SoundManager.instance.PlaySound(SelectSound);

        if(characters[ItemIndex].isUnlock == false){
            GameObject btn = Characters[ItemIndex].transform.Find("Buybtn").gameObject;

            int Price = Convert.ToInt32(btn.transform.GetChild(0).gameObject.GetComponent<Text>().text);

            if(PlayerPrefs.GetInt("Tokens") >= Price){
                PlayerPrefs.SetInt("Tokens", PlayerPrefs.GetInt("Tokens") - Price);

                ShopDM.UnlockChar(ItemIndex);
                UpdateShop();

                messageText.text = "You Successfully Bought A Skin!";
                SendMessage();
            }else{
                messageText.text = "Not Enough Coins!";
                SendMessage();
            }
        }else if(characters[ItemIndex].isUnlock == true && ItemIndex != SelectedCharacter){
            ShopDM.UpdateSelectedCharacter(ItemIndex);
            UpdateShop();
        }
        
    }

    private void SendMessage() {
        messageTextObj.SetActive(true);
        message = true;
        
    }
    


    public void ShowBullets() {
        BulletScroller.SetActive(true);
        CharScroller.SetActive(false);
        BulletBtn.interactable = false;
        CharBtn.interactable = true;
    }
    public void ShowCharacters() {
        BulletScroller.SetActive(false);
        CharScroller.SetActive(true);
        BulletBtn.interactable = true;
        CharBtn.interactable = false;
    }
}
