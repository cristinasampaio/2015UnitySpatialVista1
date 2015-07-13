import UnityEngine

class placement (MonoBehaviour): 

	def Start ():
		if (GameObject.Find("Office_Master").GetComponent[of officemaster]().Getlastroom() == "reception_room"):
			if(Application.loadedLevelName == "entrance_desk"):
				gameObject.transform.localPosition = Vector3(22.16,13.8,0.74)
				gameObject.transform.localEulerAngles = Vector3(0,-90,0)
			else:
				gameObject.transform.localPosition = Vector3(18.13,10.5757,-21.134)
				gameObject.transform.localEulerAngles = Vector3(0,-90,0)
		elif (GameObject.Find("Office_Master").GetComponent[of officemaster]().Getlastroom() == "meeting room"):
			gameObject.transform.localPosition = Vector3(-3.28,10.77,-28.41)
			gameObject.transform.localEulerAngles = Vector3(0,0,0)
		elif(GameObject.Find("Office_Master").GetComponent[of officemaster]().Getlastroom() == "office"):
			gameObject.transform.localPosition = Vector3(-31.97,8.65,41.91)
			gameObject.transform.localEulerAngles = Vector3(0,180,0)
				
