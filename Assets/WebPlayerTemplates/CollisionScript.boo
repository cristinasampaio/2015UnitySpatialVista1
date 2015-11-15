import UnityEngine
#Deprecated code I believe.
class CollisionScript (MonoBehaviour): 
	val as GameObject
	
	def Start():
		val = GameObject.Find("GameMaster")

	def OnTriggerEnter(other as Collider):
		if (other.tag == "Player"):
			val.gameObject.SendMessage("loadNewScene")
