import UnityEngine

class AddScript (MonoBehaviour): 

	def Start ():
		for obj in GameObject.FindGameObjectsWithTag('target'):
			if obj.GetComponent[of WriteCoordinates]() == null:
				obj.AddComponent[of WriteCoordinates]()
				obj.GetComponent[of WriteCoordinates]().checkColliders()
		for obj in GameObject.FindGameObjectsWithTag('nontarget'):
			if obj.GetComponent[of WriteCoordinates]() == null:
				obj.AddComponent[of WriteCoordinates]()
				obj.GetComponent[of WriteCoordinates]().checkColliders()