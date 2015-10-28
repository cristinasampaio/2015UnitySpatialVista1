import UnityEngine
#What this script does is mostly self-explanatory. Each object that the player can interact with
#ie pick up and put down
#is tagged target or nontarget. The only difference between the two is that target will write data out to the csv file.
#-Jacob


class AddScript (MonoBehaviour): 

	def Start ():
		for obj in GameObject.FindGameObjectsWithTag('target'):
			if obj.GetComponent[of WriteCoordinatesOLD]() == null:
				obj.AddComponent[of WriteCoordinatesOLD]()
				obj.GetComponent[of WriteCoordinatesOLD]().checkColliders()
		for obj in GameObject.FindGameObjectsWithTag('nontarget'):
			if obj.GetComponent[of WriteCoordinatesOLD]() == null:
				obj.AddComponent[of WriteCoordinatesOLD]()
				obj.GetComponent[of WriteCoordinatesOLD]().checkColliders()