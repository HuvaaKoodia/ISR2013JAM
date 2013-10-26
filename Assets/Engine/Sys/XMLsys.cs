using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using System.Linq;

public class XMLsys : MonoBehaviour {
	
	public DialogueDatabase dialoguedatabase;
	
	
	//engine logic
	void Awake () {
		readXML();
	}
	
	void OnDestroy(){
		writeXML();
	}
	
	//game logic
	public void readXML(){
		
		dialoguedatabase.InitDialogueDataBase();
		
		var path="Data/Dialogues";
		
		var files=Directory.GetFiles(path);
		foreach (var file in files){
			if(file.EndsWith("Example.xml")) continue;
			
			var Xdoc=new XmlDocument();
			try{
			Xdoc.Load(file);
			}
			catch(Exception e){
				Debug.LogError("File: "+file+" not functional\n"+e.StackTrace);
			}
				
			var dialogues=Xdoc.GetElementsByTagName("Data");
			
			foreach (XmlNode d in dialogues){
				try{
					var name=d["Name"].InnerText;
					
					var text="";
					if (d["Text"]!=null)
						text=d["Text"].InnerText;
					var type="";
					if (d["Type"]!=null)
						type=d["Type"].InnerText;
					
					var data=new DialogueData(name);
					data.Text=text;
					data.Type=type;
					
						//reading links
					foreach (XmlNode n in d.ChildNodes){
						if (n.Name!="Link") continue;
						int chance=0;
						if(n.Attributes["Random"]!=null){
							chance=int.Parse(n.Attributes["Random"].Value);
						}
	
	                    data.AddLink(new DialogueLink(n.InnerText, chance));
					}
				
					dialoguedatabase.AddDialogueData(name,data);
				}
				catch(Exception e){
					Debug.LogError("Dialogue data is faulty!\n"+e.Message);
					break;	
				}
			}
		}
		
		dialoguedatabase.ParseDialogueDataBase();
	}
	
	public void writeXML(){
		//example dialogue d
		var path="Data/Dialogues";
		checkFolder(path);
		
		var file="/Example.xml";
		if (checkFile(path+file)) return;
		
		XmlDocument Xdoc=new XmlDocument();
		
		var root=Xdoc.CreateElement("Root");
		Xdoc.AppendChild(root);
		
		var data=addElement(root,"Data");
		
		addElement(data,"Name","DialogueName");
		addElement(data,"Text","DialogueText");
		addElement(data,"Type","DialogueType");
		
		var links=addElement(data,"Links");
		var comment=Xdoc.CreateComment("The link's random value has an effect only if the dialogue type is RANDOM");
		links.AppendChild(comment);
		var link=addElement(links,"Link","DialogueName2");
		addAttribute(link,"random","80");
		addElement(links,"Link","DialogueName3");

		Xdoc.Save(path+file);
	}
	
	//subs
	string getStr(XmlElement element,string name){
		if (element[name]==null) return "";
		return element[name].InnerText;
	}
	
	int getInt(XmlElement element,string name){
		if (element[name]==null) return 0;
		return int.Parse(element[name].InnerText);
	}
	
	float getFlt(XmlElement element,string name){
		if (element[name]==null) return 0f;
		return float.Parse(element[name].InnerText);
	}
	
		
	//adding elements
	XmlElement addElement(XmlElement element,string name){
		var node=element.OwnerDocument.CreateElement(name);
		element.AppendChild(node);
		return node;
	}
	
	XmlElement addElement(XmlElement element,string name,string val){
		var node=element.OwnerDocument.CreateElement(name);
		node.InnerText=val;
		element.AppendChild(node);
		return node;
	}
	
	XmlElement addElement(XmlElement element,string name,int val){
		return addElement(element,name,val.ToString());
	}
	
	XmlElement addElement(XmlElement element,string name,float val){
		return addElement(element,name,val.ToString());
	}
	//adding attributes
		
	XmlAttribute addAttribute(XmlElement element,string name,string val){
		var att=element.OwnerDocument.CreateAttribute(name);
		att.Value=val;
		element.Attributes.Append(att);
		return att;
	}

	XmlAttribute addAttribute(XmlElement element,string name,int val){
		return addAttribute(element,name,val.ToString());
	}
	
	XmlAttribute addAttribute(XmlElement element,string name,float val){
		return addAttribute(element,name,val.ToString());
	}
	
		
	void readAuto(XmlElement element,object obj){
		foreach (var f in obj.GetType().GetFields()){
			if (f.IsPublic){
				if (element[f.Name]!=null){
					f.SetValue(obj,Convert.ChangeType(element[f.Name].InnerText,f.FieldType));
				}
			}
		}
	}
	
	void writeAuto(XmlElement element,object obj){
		foreach (var f in obj.GetType().GetFields()){
			addElement(element,f.Name,f.GetValue(obj).ToString());
		}
	}
	
	void readAutoFile(string path,string folder,string file,object obj){
		if (folder!="")
			folder=@"\"+folder;
		if (Directory.Exists(path+folder)){
			file=@"\"+file+".xml";
			
			var Xdoc=new XmlDocument();
			Xdoc.Load(path+folder+file);
			
			var root=Xdoc["Stats"];
			
			readAuto(root,obj);
		}
		
	}
	
	void writeAutoFile(string path,string folder,string file,object obj){
		if (folder!="")
			folder=@"\"+folder;
		checkFolder(path+folder);

		file=@"\"+file+".xml";
		var Xdoc=new XmlDocument();
		var root=Xdoc.CreateElement("Stats");
		
		writeAuto(root,obj);
		
		Xdoc.AppendChild(root);
		Xdoc.Save(path+folder+file);
	}
	
	/// <summary>
	/// Creates a folder if it doesn't exist
	/// </summary>
	void checkFolder(string path){
		if (!Directory.Exists(path)){
			Directory.CreateDirectory(path);
		}
	}
	
	bool checkFile(string path){
		return File.Exists(path);
	}
}
