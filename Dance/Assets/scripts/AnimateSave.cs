using UnityEngine;
using System.Collections;

public class AnimateSave : MonoBehaviour {

    public Transform TopLevel;

    public Transform[] bones;

	// Use this for initialization
	void Start () {
        Transform[] currentBones = GetComponentsInChildren<Transform>();
        bones = new Transform[currentBones.Length];
        print(currentBones.Length);
        for(int i = 0; i < currentBones.Length; i ++)
        {
            bones[i] = new GameObject().transform;
            bones[i].name = currentBones[i].name;
            bones[i].position = new Vector3(currentBones[i].position.x, currentBones[i].position.y, currentBones[i].position.z);
            bones[i].rotation = currentBones[i].rotation;
            Vector3 v = bones[i].rotation.eulerAngles;
     //       print(bones[i].name + "local: " + bones[i].localPosition + " world: " + bones[i].position + " transform: " + bones[i].TransformPoint(bones[i].localPosition));
        }
        //  leftArm.position = new Vector3(leftArm.position.x + 10, leftArm.position.y + 10, leftArm.position.z + 10);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    
    void SaveBones ()
    {
        bones = GetComponentsInChildren<Transform>();
    }


    public void ChangeBones ()
    {
        print("调用");
        Transform[] currentBones = GetComponentsInChildren<Transform>();
        print(currentBones.Length);
        for(int i = currentBones.Length - 1 ; i >= 0; i --)
        {
            if (currentBones[i].name == "Bip01 L Hand")
            {
                print(currentBones[i].name + currentBones[i].position + bones[i].position);
                iTween.MoveTo(currentBones[i].gameObject, bones[i].position, 1);
                iTween.RotateTo(currentBones[i].gameObject, bones[i].rotation.eulerAngles, 1);
                currentBones[i].position = bones[i].position;
                print(currentBones[i].name + currentBones[i].position + bones[i].position);
            }
            if (currentBones[i].position != bones[i].position)
            {
            //    print(currentBones[i].name + currentBones[i].position + bones[i].position);
                
          /*      iTween.MoveAdd(currentBones[i].gameObject, iTween.Hash("x", - bones[i].position.x + currentBones[i].position.x,
                                                                      "y", -bones[i].position.y + currentBones[i].position.y,
                                                                      "z", -bones[i].position.z + currentBones[i].position.z,
                                                                      "easeType", "easeInOutExpo", 
                                                                      "loopType", "pingPong", 
                                                                      "delay", .1));
                iTween.RotateBy(currentBones[i].gameObject, currentBones[i].position - bones[i].rotation.eulerAngles, 1);*/
            }
            
        }
    }
}   
