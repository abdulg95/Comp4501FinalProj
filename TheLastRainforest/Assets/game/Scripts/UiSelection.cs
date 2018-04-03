using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UiSelection : MonoBehaviour {

    // Use this for initialization
    public Image SelectionImage ;
    public Button mnkbtn;
    public Button gorbtn;
    public Button mudbtn;
    private bool mnkselect, gorselect, mudselect;

    
    public void MonkeySelection()
    {
        mnkbtn.Select();
        mnkselect = true;
    }

    public void GorillaSelection()
    {
        gorbtn.Select();
        gorselect = true;
    }

    public void MudSelection()
    {
        mudbtn.Select();
        mudselect = true;
    }

    //Do this when the selectable UI object is selected.
   
    void Start () {
        //mnkbtn.interactable = false;
        //gorbtn.interactable = false;
        //mudbtn.interactable = false;
        //stay uninteractable till enough gold
        mnkselect = false;
        gorselect = false;
        mudselect = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
        if(mnkselect == true)
        {
            //spawnmonkeymode code here
            mudselect = false;
            gorselect = false;
        }
        if (gorselect == true)
        {
            //spawngorillamode code here
            //deduct gold in spawn gorilla code
            mnkselect = false;
            mudselect = false;

        }
        if (mudselect == true)
        {
            //switch back to normal ability here
            mnkselect = false;
            gorselect = false;

        }

    }

    // Update is called once per frame
    void Update () {
		//make interactable here based on gold
	}
}
