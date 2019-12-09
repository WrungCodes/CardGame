using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardList : MonoBehaviour
{
    // Start is called before the first frame update

    public List<CardObj> cardObjs;

    public bool isMine = false;

    public string PlayerName;

    //public Text nameText;

    public TextMesh nameText;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNameText(string name)
    {
        PlayerName = name;

        nameText.text = name;
    }
}
