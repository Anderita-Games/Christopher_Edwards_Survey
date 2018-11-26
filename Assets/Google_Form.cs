using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Google_Form : MonoBehaviour {
	public string[] Data; //Responses
	public UnityEngine.UI.InputField[] Application_Data;
	string BASE_URL = "https://docs.google.com/forms/d/1SJAU88_Pe3-pv0eizsIFp63QdzlZg12GlNj2rWbWzVc/formResponse";

	public void Start () {
		if (PlayerPrefs.GetString("Survey Taken") == "TRUE") { //Prevents retaking the survey
			//SceneManager.LoadScene("Contact Card");
		}
	}

	public void Send () {
		StartCoroutine(Submit());
		PlayerPrefs.SetString("Survey Taken", "TRUE");
		SceneManager.LoadScene("Contact Card");
	}

	IEnumerator Submit () {
		WWWForm Form = new WWWForm();
		//APPLICATION INFO
		Form.AddField("entry.344570279", Data[0]);   //NAME
		Form.AddField("entry.511700238", Data[1]);   //PARENT'S NAME
		Form.AddField("entry.1075941252", Data[2]);  //PARENTS' CONTACT'
		Form.AddField("entry.67758901", Data[3]);    //SCHOOL ATTENDED
		Form.AddField("entry.2146623077", Data[4]);  //CITY
		//SURVEY INFO
		Form.AddField("entry.556729619", Data[5]);   //BREAKFAST?
		Form.AddField("entry.399886256", Data[6]);   //WHAT WILL YOU APPLY?
		Form.AddField("entry.933347346", Data[7]);   //MOTIVATED?
		Form.AddField("entry.1164357255", Data[8]);  //KNOWLEDGABLE?
		Form.AddField("entry.1345923568", Data[9]);  //RELATABLE?
		Form.AddField("entry.1461087360", Data[10]); //WILLINGNESS TO HELP?
		Form.AddField("entry.1630399567", Data[11]); //AWARE OF PROBLEM?
		Form.AddField("entry.1738241086", Data[12]); //WHAT YOU BECOME?
		Form.AddField("entry.1946901609", Data[13]); //LIFE PROOF PLAN?
		Form.AddField("entry.1860197642", Data[14]); //HEALTHY?
		Form.AddField("entry.172161094", Data[15]);  //HEALTHY LIFE CHOICES?
		byte[] rawData = Form.data;
		WWW WWW = new WWW(BASE_URL, rawData);
		yield return WWW;
	}

	public void Application_Next () {
		for (int i = 0; i < Application_Data.Length; i++) {
			Data[i] = Application_Data[i].text;
		}
		//Hide this gameobject and move on
	}
}
