import UnityEngine

class Phone (MonoBehaviour): 
	public phone as AudioClip
	source as AudioSource
	def Start ():
		source = gameObject.GetComponent[of AudioSource]()
	
	def Update ():
		pass
	def ring():
		if (GetComponent[of AudioSource]().isPlaying == true) :
			GetComponent[of AudioSource]().Stop()
		else:		
			GetComponent[of AudioSource]().clip = phone;
			GetComponent[of AudioSource]().Play ()