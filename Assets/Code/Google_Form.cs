using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Google_Form : MonoBehaviour {
	public string[] Data; //Responses
	public GameObject[] UI_Elements; //All UI Stuff
	public UnityEngine.UI.Text[] Application_Options;
	int Question_Number = 0;
	string[] Questions;
	public UnityEngine.UI.Text Survey_Question;
	string BASE_URL = "https://docs.google.com/forms/d/1SJAU88_Pe3-pv0eizsIFp63QdzlZg12GlNj2rWbWzVc/formResponse";

	public void Start () {
		if (PlayerPrefs.GetString("Survey Taken") == "TRUE") { //Prevents retaking the survey
			SceneManager.LoadScene("Contact Card");
		}
		Questions = new string[] { //APPLICATION SECTION
			//YES OR NO
			"Did you have breakfast?",
			"Did Chris Nolan motivate you to succeed?",
			"Can you relate to Chris Nolan?",
			"Did Chris Nolan make you aware of the problem?",
			"Did Chris Nolan challenge you to pick a career goal or ask you what you would like to become?",
			"Did Chris Nolan motivate you to think/build a fool proof life plan?",
			"Does Chris Nolan seem healthy?",
			"Did Chris Nolan teach/expose you to healthy lifestyle choices?",
			//5 OPTIONS
			"Will you apply what you learned from the lecture?",
			"Is Chris Nolan Knowledgeable?",
			"Did Chris Nolan have a willingness to help?"
		};
	}

	IEnumerator Submit () {
		WWWForm Form = new WWWForm();
		//APPLICATION INFO
		Form.AddField("entry.344570279", Data[0]);   //NAME
		Form.AddField("entry.511700238", Data[1]);   //PARENT'S NAME
		Form.AddField("entry.1075941252", Data[2]);  //PARENT'S CONTACT
		Form.AddField("entry.67758901", Data[3]);    //SCHOOL ATTENDED
		Form.AddField("entry.2146623077", Data[4]);  //CITY
		//SURVEY INFO
		Form.AddField("entry.556729619", Data[5]);   //BREAKFAST?
		Form.AddField("entry.933347346", Data[6]);   //MOTIVATED?
		Form.AddField("entry.1345923568", Data[7]);  //RELATABLE?
		Form.AddField("entry.1630399567", Data[8]);  //AWARE OF PROBLEM?
		Form.AddField("entry.1738241086", Data[9]);  //WHAT YOU BECOME?
		Form.AddField("entry.1946901609", Data[10]); //LIFE PROOF PLAN?
		Form.AddField("entry.1860197642", Data[11]); //HEALTHY?
		Form.AddField("entry.172161094", Data[12]);  //HEALTHY LIFE CHOICES?
		Form.AddField("entry.399886256", Data[13]);  //WHAT WILL YOU APPLY?
		Form.AddField("entry.1164357255", Data[14]); //KNOWLEDGABLE?
		Form.AddField("entry.1461087360", Data[15]); //WILLINGNESS TO HELP?
		byte[] rawData = Form.data;
		WWW WWW = new WWW(BASE_URL, rawData);
		yield return WWW;
	}

	public void Application_Next () { //APPLICATION SECTION
		bool Complete = true;
		for (int i = 0; i < 16; i++) { //NULL CHECK
			if (i <= 11) {
				if (i % 2 == 0 && i != 0 && UI_Elements[i].transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text == "") {
					Complete = false;
				}
			}
		}
		if (Complete == true) {
			for (int i = 0; i < 16; i++) {
				if (i <= 11) {
					if (i % 2 == 0 && i != 0) { //DATA COLLECTION
						Data[(i-2)/2] = UI_Elements[i].transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text;
					}
					UI_Elements[i].SetActive(false);
				}else { //SHOW SURVEY
					UI_Elements[i].SetActive(true);
				}
			}
		}
	}

	public void Survey_Next (UnityEngine.UI.Button Response) { //SURVEY SECTION
		Data[5+Question_Number] = Response.GetComponentInChildren<UnityEngine.UI.Text>().text;
		Question_Number++;
		if (Question_Number != Questions.Length) {
			Survey_Question.text = Questions[Question_Number];
			if (Question_Number >= 8) { //QUESTIONS WITH 5 OPTIONS
				string[] Choices = null;
				if (Question_Number == 8) { //WHAT WILL YOU APPLY?
					UI_Elements[16].SetActive(true);
					UI_Elements[17].SetActive(true);
					UI_Elements[18].SetActive(true);
					Choices = new string[] {
						"No", "Probably not", "Maybe",
						"More thank likely", "Most definitely"
					};
				}else if (Question_Number == 9) { //KNOWLEDGABLE?
					Choices = new string[] {
						"No", "Not really", "Kind of",
						"Very Knowledgeable", "Very Very Knowledgeable"
					};
				}else if (Question_Number == 10) { //WILLINGNESS TO HELP?
					Choices = new string[] {
						"No", "Not really", "Kind of",
						"He is helpful", "Overwhelming"
					};
				}
				for (int i = 0; i < Application_Options.Length; i++) {
					Application_Options[i].text = Choices[Choices.Length-i-1];
				}
			}
		}else {
			StartCoroutine(Submit());
			PlayerPrefs.SetString("Survey Taken", "TRUE");
			SceneManager.LoadScene("Contact Card");
		}
	}
}
