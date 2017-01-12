using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour {

    public int status = 0;
    public Texture2D icon;
    GUIStyle gs = new GUIStyle();

    public Texture2D icon_empty;
    GUIStyle gs_empty = new GUIStyle();
    private string str = "";

    public bool record = false;
    private int volumn = 0;

    // Use this for initialization
    void Start () {
        gs.normal.background = icon;
        gs_empty.normal.background = icon_empty;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0))
        {
            if(Input.mousePosition.x > 500 && Input.mousePosition.y < 250)
            {
                print("开始");
                str += "弯曲左臂";
                record = true;
                //       GameObject.Find("Main Camera").GetComponent<XunFei>().DisplayResult(str);
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                jo.Call("StartRecognize");

                
            }
            print(Input.mousePosition.x + " " + Input.mousePosition.y);
        }
	}

    public void OnGUI()
    {
        if (GUI.Button(new Rect(500, 1050, 200, 200), icon, gs))
        {
            str += "放手";
            record = false;
            //     GameObject.Find("Main Camera").GetComponent<XunFei>().DisplayResult(str);
            //        GameObject.Find("origin_1").GetComponent<AnimateSave>().ChangeBones();
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("StopRecognize");
        }
        if (record)
        {
            if(volumn < 15) 
                GUI.Button(new Rect(140, 400, 400, 400), icon_empty);
            else
                GUI.Button(new Rect(140, 400, 400, 400), icon);
        }
    }

    public void setVolumn(string num)
    {
        volumn = int.Parse(num);
    }

 

}
