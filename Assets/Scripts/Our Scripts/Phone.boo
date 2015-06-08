import UnityEngine

class Phone (MonoBehaviour): 
	public phone as AudioClip
	source as AudioSource
	def Start ():
		source = gameObject.GetComponent[of AudioSource]()
	
	def Update ():
		pass
	def ring():
		if (audio.isPlaying == true) :
			audio.Stop()
		else:		
			audio.clip = phone;
			audio.Play ()