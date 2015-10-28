using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class tutWrite : MonoBehaviour {

    static bool first = true;
	// Use this for initialization
	void Start () {
        if (first)
        {
            first = false;
            try
            {
                using (System.IO.StreamWriter myfile = File.AppendText("Data/data.csv"))
                {
                    myfile.WriteLine("Tutorial Placement Data");
                }
            }
            catch
            {
            }
        }
	}
	
	// Update is called once per frame
	public void WritePosition() {
        try
        {
            using (System.IO.StreamWriter myfile = File.AppendText("Data/data.csv"))
            {
                myfile.WriteLine(gameObject.transform.localPosition.x + "," + gameObject.transform.localPosition.y + "," + gameObject.transform.localPosition.z);
            }
        }
        catch
        {
        }
	}
}
