using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class XunFei : MonoBehaviour {

    public InputField Result;
    public InputField Status;
    public Animator ani;
    public AudioSource audio1;
    public AudioSource audio2;
    public Texture2D icon1;
    GUIStyle ggs = new GUIStyle();
    public Texture2D icon2;
    GUIStyle gggs = new GUIStyle();
    public Texture2D icon3;
    GUIStyle ggggs = new GUIStyle();

    private int length = 0;
    private float time = 0.0f;
    private float shakeTime = 0.0f;
    private bool idle = true;
    private bool shake = false;
    private int headStatus = 1;

    private bool begin = false;

    private bool intTime = false;
    private int countDown = 10000;

    public string currentStatus = "";

	// Use this for initialization
	void Start () {

        //   iTween.MoveBy(gameObject, iTween.Hash("z", 34, "time", 3));
        //   audio2.Play();
        //  audio1.Stop();
        //   setIdle(true);

        ggs.normal.background = icon1;
        gggs.normal.background = icon2;
        ggggs.normal.background = icon3;
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("StartActivity1");
        Result = GameObject.Find("Result").GetComponent<InputField>();
        Status = GameObject.Find("Status").GetComponent<InputField>();

        // 创建计时器  
        Timer timer = Timer.createTimer("Timer");
        //开始计时  
        timer.startTiming(countDown, OnComplete, OnProcess);
    }

    // Update is called once per frame
    void Update()
    {
        if(idle)
        {
            time += Time.deltaTime;
            if (time > 5)
            {
                ani.SetBool("Idle_long", true);
            }
        }
        else
        {
            time = 0;
        }

        if (shake)
        {
            shakeTime += Time.deltaTime;
            if (shakeTime > 4)
            {
                ani.SetBool("ShakeHead", false);
                ani.SetBool("Idle", true);
                ani.SetBool("Idle_long", false);
                idle = true;
                shake = false;
            }
        }
        else
        {
            shakeTime = 0;
        }
    }

    public void DisplayResult(string str)
    {
        Result.text = str;
        //开始
        if (str.Substring(length).ToLower().IndexOf("begin") > -1
            || str.Substring(length).ToLower().IndexOf("began") > -1)
        {
            begin = true;
            setIdle(true);
            audio1.Stop();
            audio2.Play();
            iTween.MoveBy(gameObject, iTween.Hash("z", 34, "time", 3));
        }
        if (begin)
        {
            //头部动作
            if (str.Substring(length).ToLower().IndexOf("head") > -1 && str.Substring(length).ToLower().IndexOf("down") > -1)
            {
                if (headStatus == 1)
                {
                    ani.SetBool("Head_down", true);
                    headStatus = 0;
                }
                else
                {
                    ani.SetBool("Head_up", false);
                    headStatus = 1;
                }

            }
            else if (str.Substring(length).ToLower().IndexOf("head") > -1 && str.Substring(length).ToLower().IndexOf("up") > -1)
            {
                if (headStatus == 1)
                {
                    ani.SetBool("Head_up", true);
                    headStatus = 2;
                }
                else
                {
                    ani.SetBool("Head_down", false);
                    headStatus = 1;
                }

            }

            //手部动作
            else if (str.Substring(length).ToLower().IndexOf("hand") > -1 || str.Substring(length).ToLower().IndexOf("arm") > -1)
            {
                //抬手
                if (str.Substring(length).ToLower().IndexOf("up") > -1 
                    || str.Substring(length).ToLower().IndexOf("raise") > -1
                    || str.Substring(length).ToLower().IndexOf("rise") > -1
                    || str.Substring(length).ToLower().IndexOf("lift") > -1
                    || str.Substring(length).ToLower().IndexOf("stretch") > -1)
                {
                    if (str.Substring(length).ToLower().IndexOf("left") > -1)
                    {
                        if (str.Substring(length).ToLower().IndexOf("side") > -1)
                        {
                            ani.SetBool("Larm_1", true);
                        }
                        else if (str.Substring(length).ToLower().IndexOf("front") > -1
                                || str.Substring(length).ToLower().IndexOf("stretch") > -1)
                        {
                            ani.SetBool("Larm_2", true);
                        }
                        else
                        {
                            ani.SetBool("Larm_1", true);
                        }
                    }
                    else if(str.Substring(length).ToLower().IndexOf("right") > -1)
                    {
                        if (str.Substring(length).ToLower().IndexOf("side") > -1)
                        {
                            ani.SetBool("Rarm_1", true);
                        }
                        else if (str.Substring(length).ToLower().IndexOf("front") > -1 
                            || str.Substring(length).ToLower().IndexOf("stretch") > -1)
                        {
                            ani.SetBool("Rarm_2", true);
                        }
                        else
                        {
                            ani.SetBool("Rarm_1", true);
                        }
                    }
                    else
                    {
                        ani.SetBool("Larm_1", true);
                        ani.SetBool("Rarm_1", true);
                    }
                }
                //向后摆手
                else if (str.Substring(length).ToLower().IndexOf("back") > -1)
                {
                    if(str.Substring(length).ToLower().IndexOf("left") > -1)
                    {
                        ani.SetBool("Larm_3", true);
                    }
                    else if(str.Substring(length).ToLower().IndexOf("right") > -1)
                    {
                        ani.SetBool("Rarm_3", true);
                    }
                    else
                    {
                        ani.SetBool("Larm_3", true);
                        ani.SetBool("Rarm_3", true);
                    }
                }
                //弯曲手臂
                else if (str.Substring(length).ToLower().IndexOf("bend") > -1
                        || str.Substring(length).ToLower().IndexOf("curve") > -1)
                {
                    if(str.Substring(length).IndexOf("left") > -1)
                        ani.SetBool("Larm_4", true);
                    else if(str.Substring(length).IndexOf("right") > -1)
                        ani.SetBool("Rarm_4", true);
                    else
                    {
                        ani.SetBool("Larm_4", true);
                        ani.SetBool("Rarm_4", true);
                    }
                }
                //放手
                else if (str.Substring(length).ToLower().IndexOf("down") > -1)
                {
                    if (str.Substring(length).ToLower().IndexOf("left") > -1)
                    {
                        ani.SetBool("Larm_1", false);
                        ani.SetBool("Larm_2", false);
                        ani.SetBool("Larm_3", false);
                        ani.SetBool("Larm_4", false);
                    }
                    else if (str.Substring(length).ToLower().IndexOf("right") > -1)
                    {
                        ani.SetBool("Rarm_1", false);
                        ani.SetBool("Rarm_2", false);
                        ani.SetBool("Rarm_3", false);
                        ani.SetBool("Rarm_4", false);
                    }
                    else
                    {
                        ani.SetBool("Larm_1", false);
                        ani.SetBool("Larm_2", false);
                        ani.SetBool("Larm_3", false);
                        ani.SetBool("Larm_4", false);

                        ani.SetBool("Rarm_1", false);
                        ani.SetBool("Rarm_2", false);
                        ani.SetBool("Rarm_3", false);
                        ani.SetBool("Rarm_4", false);
                    }

                    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                    jo.Call("StartSpeak", "吓死你");

                    WaitAndIdle(1.0f);

                }
            }
            //转身动作
            else if (str.Substring(length).ToLower().IndexOf("twist") > -1
                    || str.Substring(length).ToLower().IndexOf("turn") > -1)
            {
                if (str.Substring(length).ToLower().IndexOf("left") > -1)
                    ani.SetBool("Ltwist", true);
                else if (str.Substring(length).ToLower().IndexOf("right") > -1)
                    ani.SetBool("Rtwist", true);
                else
                    ani.SetBool("Ltwist", true);
            }
            //回到正面
            else if (str.Substring(length).ToLower().IndexOf("back") > -1)
            {
                setIdle(true);
                ani.SetBool("Rtwist", false);
                ani.SetBool("Ltwist", false);
                WaitAndIdle(1.0f);
            }
            //彩蛋
            else if (str.Substring(length).ToLower().IndexOf("put") > -1
                    && str.Substring(length).ToLower().IndexOf("off") > -1)
            {
                ani.SetBool("ShakeHead", true);
                setIdle(false);
                shake = true;
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                jo.Call("StartSpeak", "I can't do it.");
            }
            //听不懂
            else if (str.Substring(length).ToLower().IndexOf("begin") <= -1
                && str.Substring(length).ToLower().IndexOf("began") <= -1)
            {
                ani.SetBool("ShakeHead", true);
                setIdle(false);
                shake = true;
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                jo.Call("StartSpeak", "Sorry");
            }
        }
        length = str.Length;
    }

    public void OnGUI()
    {
        if (!begin)
        {
            GUI.Button(new Rect(150, 50, 400, 400), icon1, ggs);
            GUI.Button(new Rect(125, 950, 470, 30), icon2, gggs);
            GUI.Button(new Rect(150, 470, 400, 100), icon3, ggggs);
        }
    }

    public void setIdle(bool status)
    {
        ani.SetBool("Idle", status);
        ani.SetBool("Idle_long", status);
        idle = status;
    }

    //定义 WaitAndIdle（）方法    
    IEnumerator WaitAndIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //等待之后执行的动作    
        setIdle(true);
    }

    public void DisplayStatus(string str)
    {
        currentStatus = currentStatus + str + "\n";
        Status.text = str;
    }

    // 计时结束的回调  
    void OnComplete()
    {
        Debug.Log("complete !");
    }
    // 计时器的进程  
    void OnProcess(float p)
    {
        Debug.Log("on process " + p);
        if ((countDown * p - Math.Round(countDown * p)) < 0.001)
            intTime = true;
        else
            intTime = false;
       
    }

}
