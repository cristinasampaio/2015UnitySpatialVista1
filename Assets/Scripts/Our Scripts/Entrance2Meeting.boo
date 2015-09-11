import UnityEngine

class Entrance2Meeting (MonoBehaviour): 

	def Start ():
		pass
	
	def OnTriggerEnter(other as Collider):
		if (other.tag == "Player"):
			if(gameObject.name == "meetDoor"):
				Application.LoadLevel("meeting room")
			elif(gameObject.name == "wait_door"):
				Application.LoadLevel("reception_room")
			elif(gameObject.name == "entrDoor"):
				Application.LoadLevel("entrance_desk")
			elif(gameObject.name == "entrDoor2"):
				Application.LoadLevel("entrance_desk")
			elif(gameObject.name == "officedoor"):
				Application.LoadLevel("office")
			elif(gameObject.name == "waitdoor2"):
				Application.LoadLevel("reception_room")
		//	GameObject.Find("Office_Master").GetComponent[of officemaster]().Setlastroom()
			
