import UnityEngine

public class keeper:
#This is a simple static class designed to be accessed by the scripts on each object.
#Basically storing the positions of each object.
#During Phase 2 of the test, users can pick up and drop objects, in addition to objects being scattered across the room.
#So at the start the object reads from the pos to see where it should be, then writes back to it when the playr drops the object somewhere.
#Another case where it's probably a better idea to write this stuff out to a CSV file, except reading and writing becomes a pain.
#Might be a better idea to just read this data in from a file at run time into a list or hash table.
#Script has to be refactored in the future due to Boo anyways.
#-Jacob
	static apartmentpos = {"loveseat" : Vector3(-3.3587,-3.066,2.1599),"plateundertv" : Vector3(0.088,-2.766,-0.092),"smallbook" : Vector3(-0.179,-0.16,2.362), "sidetable" : Vector3(0.3603,-3.104,2.3413),"creamybunbox" : Vector3(-0.15,1.799,0.793), "longcanvas" : Vector3(-2.793,-1.403,-3.692),"outsidevase" : Vector3(1.631,0.395,12.34),"knifeblock" : Vector3(-5.701,0.435,-4.754),"cereal" : Vector3(0.003,1.795,-0.009),"sugar" : Vector3(0.028,1.795,0.137),"chips" : Vector3(0.032,1.797,0.285),"beer" : Vector3(0.032,1.8,0.448),"bread" : Vector3(0.026,1.807,0.631),"coffeecan" : Vector3(0.079,1.807,0.801),"salami" : Vector3(-0.121,1.833,0.878),"fruitbowl" : Vector3(3.476,1.527,-1.854),"table" : Vector3(-0.151,-0.4871,-7.345),"chaira" : Vector3(-0.129,-0.4877,-6.908),"chairb" : Vector3(0.57,-0.487,-7.335),"chairc" : Vector3(-0.108,-0.487,-7.823),"chaird" : Vector3(-0.916,-0.487,-7.416)}
	static apartmentrot = {"loveseat" : Vector3(0,216.81,0),"smallbook" : Vector3(0.44370,241.666,269.256),"sidetable" : Vector3(-90,-180,0), "creamybunbox" : Vector3(0,275.25,0),"longcanvas" : Vector3(270,539.96,0),"cereal" : Vector3(0,0,0),"sugar" : Vector3(0,91.014,0),"bagofchips" : Vector3(0,90,0),"beer" : Vector3(-0.204,90,-0.717),"bread" : Vector3(0,271.40,0),"coffeecan" : Vector3(0,72.950,0),"fruitbowl" : Vector3(270,0,0),"table" : Vector3(270,179.84,0),"chaira" : Vector3(270,180,0),"chairb" : Vector3(270,268.06,0),"chairc" : Vector3(270,0,0),"chaird" : Vector3(270,90.466,0)}
	static officepos = {"printer" : Vector3(-17.61,0.59,40.77),"mouse" : Vector3(19.19,6.4,27.25), "chair2" : Vector3(20.74,0.36,30),"monitor2" : Vector3(8.77,11.89,16.88),"chair1" : Vector3(9.81,0.57,61.1),"cup" : Vector3(0.34,0.41,55.02),"keyboard1" : Vector3(5.27,0.63,48.08),"monitor1" : Vector3(-5.16,1.06,33.72),"paper263" : Vector3(35.22,0.18,24.92),"tablet1" : Vector3(-17.05,-0.88,-11.23),"trashbasketmeeting" : Vector3(18.87,0,6.39),"plant2" : Vector3(-15.7,1.95,63.53),"plant" : Vector3(-14.55,2.11,19.34),"paper229" : Vector3(-18.8,1.64,26.91),"bluebinders" : Vector3(39.32,2.05,44.99),"chair3" : Vector3(13.98,6.3,-16.47),"wallpicture" : Vector3(0.93,8.97,68.3),"chair6" : Vector3(44.9,4.7,13.3),"officechair" : Vector3(2.49,0.55,-20.87),"chair" : Vector3(-14.48,0.4,2.36),"L&Jposter" : Vector3(51.6,12.2,5.6),"bluechair1" : Vector3(19.72,2.68,1.03),"bluechair2" : Vector3(22.73,5.38,18.66),"pencilholder" : Vector3(-14.05,0.32,16.9),"trashbasket" : Vector3(-7.55,0.71,-2.99),"chair10" : Vector3(10.6,0.4,-22.2),"cup1" : Vector3(-4.21,8.36,15.07),"phone_2" : Vector3(-35.86,9.92,2.35),"trophe" : Vector3(-38.64,10.86,45.76),"plant1" : Vector3(-23.45,3.57,0.44),"bookletstack" : Vector3(16.92,-11.51,0.11),"bookletstack1" : Vector3(18.41,-23.82,-1.25),"folders" : Vector3(11.16,6.65,-1.39),"vase4" : Vector3(-14.38,0.57,-22.61),"vase3" : Vector3(-8.05,-0.07,-22.97),"whitechair" : Vector3(-11.27,4.41,23.76),"floorlight" : Vector3(-18.45,0.09,-19.09),"books" : Vector3(-29.83,5.07,6.58)}
	static officerot = {"printer" : Vector3(270,103.86,0),"mouse" : Vector3(270,270,0), "chair2" : Vector3(270,195,0),"monitor2" : Vector3(370.84,464.99,-94.89),"chair1" : Vector3(270,250,0),"cup" : Vector3(355.60,270,-48.38),"keyboard1" : Vector3(275,90,270),"monitor1" : Vector3(371.55,179.37,87.301),"chair3" : Vector3(363.80,219.89,193.85),"wallpicture" : Vector3(0,180,103.033),"chair6" : Vector3(2.1733,180,120),"officechair" : Vector3(270,40,0),"L&Jposter" : Vector3(0,270,70.6294),"bluechair1" : Vector3(403.57,237.87,-136.37),"trashbasket" : Vector3(350.31,-180,-180),"cup1" : Vector3(270,0,0),"phone_2" : Vector3(270,54.693,0),"bookletstack" : Vector3(0,0,0),"folders" : Vector3(12.78,-74.62,185.80)}

	public static def ReturnPos(name as string):
		sceneName = Application.loadedLevelName
		
		if (sceneName == "Apartment01_2"):
			return apartmentpos[name]
		elif (sceneName == "entrance_desk" or "meeting room" or "reception_room"):
			return officepos[name]
		else:
			return -1
			
	public static def ReturnRot(name as string):
		sceneName = Application.loadedLevelName
		
		if (sceneName == "Apartment01_2"):
			return apartmentrot[name]
		elif (sceneName == "entrance_desk" or "meeting room" or "reception_room"):
			return officerot[name]
		else:
			return -1
			
	public static def SetPos(name as string, pos as Vector3):
		sceneName = Application.loadedLevelName
		
		if (sceneName == "Apartment01_2"):
			apartmentpos[name] = pos
		elif (sceneName == "entrance_desk" or "meeting room" or "reception_room"):
			officepos[name] = pos

	public static def SetRot(name as string, rot as Vector3):
		sceneName = Application.loadedLevelName
		
		if (sceneName == "Apartment01_2"):
			apartmentrot[name] = rot
		elif (sceneName == "entrance_desk" or "meeting room" or "reception_room"):
			officerot[name] = rot