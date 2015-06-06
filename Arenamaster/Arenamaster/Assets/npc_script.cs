using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

public class npc_script : npc_super {

	public float cap;		/* health cap */
	public  float current;

	public string examine;


	public string[] dialog;
	public string[] responses;

	public int numOfLines;
	public int numOfResps;

	public int xmlReaderCounter;

	public int currentLine;
	public int currentResp;

	void Start () {
		cap = 100;
		current = 100;
		//id = 10.0F;
		xmlReaderCounter=0;
		numOfLines = 0;
		numOfResps = 0;
		currentLine = 0;
		currentResp = 0;

		XmlReader reader = XmlReader.Create ("assets/npcdialog.xml");

		while (reader.Read()) {
			if( (reader.IsStartElement("npc"))  && (reader.GetAttribute("name")==this.gameObject.name)   ){

				numOfLines = int.Parse(reader.GetAttribute("lines"));
				numOfResps = int.Parse(reader.GetAttribute("resps"));

				dialog = new string[numOfLines];
				responses = new string[numOfResps];

				// Dialog line storage
				for(xmlReaderCounter=0; xmlReaderCounter<numOfLines; xmlReaderCounter++){
					reader.Read();
					if(reader.IsStartElement("line")){
						dialog[xmlReaderCounter]=reader.ReadString();	
					}
				}
				xmlReaderCounter=0;

				// Response line storage
				for(xmlReaderCounter=0; xmlReaderCounter<numOfResps; xmlReaderCounter++){
					reader.Read();
					if(reader.IsStartElement("resp")){
						responses[xmlReaderCounter]=reader.ReadString();
					}
				}
				xmlReaderCounter=0;


				
			}
		}


	}

	void Update () {

	}
}
