import UnityEngine

class tutorial (MonoBehaviour): 
	
	public stage1tutorial as GameObject
	public stage2tutorial as GameObject

	def Start ():
		stage = PlayerPrefs.GetInt("state")
		if(stage == 1):
			Instantiate(stage1tutorial)
		if(stage == 2):
			Instantiate(stage2tutorial)
	
