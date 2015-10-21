using UnityEngine;
using System;
using System.Collections.Generic;
public static class keeper {
	static Dictionary<string, Vector3> officePos = new Dictionary<string, Vector3>();
	static Dictionary<string, Vector3> officeRot = new Dictionary<string, Vector3>();
	static Dictionary<string, Vector3> apartmentPos = new Dictionary<string, Vector3>();
	static Dictionary<string, Vector3> apartmentRot = new Dictionary<string, Vector3>();

	static keeper()
	{
		//All office Positions
		officePos.Add ("printer", new Vector3 (-17.61f, 0.59f, 40.77f));
		officePos.Add ("mouse", new Vector3(19.19f, 6.4f, 27.25f));
		officePos.Add ("chair2", new Vector3(20.74f, 0.36f, 30f));
		officePos.Add ("monitor2", new Vector3(8.77f, 11.89f, 16.88f));
		officePos.Add ("chair1", new Vector3(9.81f, 0.57f, 61.1f));
		officePos.Add ("cup", new Vector3(0.34f, 0.41f, 55.02f));
		officePos.Add ("keyboard1", new Vector3(5.27f, 0.63f, 48.08f));
		officePos.Add ("monitor1", new Vector3(-5.16f, 1.06f, 33.72f));
		officePos.Add ("paper263", new Vector3(35.22f, 0.18f, 24.92f));
		officePos.Add ("tablet1", new Vector3(-17.05f, -0.88f, -11.23f));
		officePos.Add ("trashbasketmeeting", new Vector3(18.87f, 0f, 6.39f));
		officePos.Add ("plant2", new Vector3(-15.7f, 1.95f, 63.53f));
		officePos.Add ("plant", new Vector3(-14.55f, 2.11f, 19.34f));
		officePos.Add ("paper229", new Vector3(-18.8f, 1.64f, 26.91f));
		officePos.Add ("bluebinders", new Vector3(39.32f, 2.05f, 44.99f));
		officePos.Add ("chair3", new Vector3(13.98f, 6.3f, -16.47f));
		officePos.Add ("wallpicture", new Vector3(0.93f, 8.97f, 68.3f));
		officePos.Add ("chair6", new Vector3(44.9f, 4.7f, 13.3f));
		officePos.Add ("officechair", new Vector3(2.49f, 0.55f, -20.87f));
		officePos.Add ("chair", new Vector3(-14.48f, 0.4f, 2.36f));
		officePos.Add ("L&Jposter", new Vector3(51.6f, 12.2f, 5.6f));
		officePos.Add ("bluechair1", new Vector3(19.72f, 2.68f, 1.03f));
		officePos.Add ("bluechair2", new Vector3(22.73f, 5.38f, 18.66f));
		officePos.Add ("pencilholder", new Vector3(-14.05f, 0.32f, 16.9f));
		officePos.Add ("trashbasket", new Vector3(-7.55f, 0.71f, -2.99f));
		officePos.Add ("chair10", new Vector3(10.6f, 0.4f, -22.2f));
		officePos.Add ("cup1", new Vector3(-4.21f, 8.36f, 15.07f));
		officePos.Add ("phone_2", new Vector3(-35.86f, 9.92f, 2.35f));
		officePos.Add ("trophe", new Vector3(-38.64f, 10.86f, 45.76f));
		officePos.Add ("plant1", new Vector3(-23.45f, 3.57f, 0.44f));
		officePos.Add ("bookletstack", new Vector3(16.92f, -11.51f, 0.11f));
		officePos.Add ("bookletstack1", new Vector3(18.41f, -23.82f, -1.25f));
		officePos.Add ("folders", new Vector3(11.16f, 6.65f, -1.39f));
		officePos.Add ("vase4", new Vector3(-14.38f, 0.57f, -22.61f));
		officePos.Add ("vase3", new Vector3(-8.05f, -0.07f, -22.97f));
		officePos.Add ("whitechair", new Vector3(-11.27f, 4.41f, 23.76f));
		officePos.Add ("floorlight", new Vector3(-18.45f, 0.09f, -19.09f));
		officePos.Add ("books", new Vector3(-29.83f, 5.07f, 6.58f));
		//All office Rotations
		officeRot.Add ("printer", new Vector3 (270f, 103.86f, 0f));
		officeRot.Add ("mouse", new Vector3 (270f, 270f, 0f));
		officeRot.Add ("chair2", new Vector3 (270f, 195f, 0f));
		officeRot.Add ("monitor2", new Vector3 (370.84f, 464.99f, -94.89f));
		officeRot.Add ("chair1", new Vector3 (270f, 250f, 0f));
		officeRot.Add ("cup", new Vector3 (355.60f, 270f, -48.38f));
		officeRot.Add ("keyboard1", new Vector3 (275f, 90f, 270f));
		officeRot.Add ("monitor1", new Vector3 (371.55f, 179.37f, 87.301f));
		officeRot.Add ("chair3", new Vector3 (363.80f, 219.89f, 193.85f));
		officeRot.Add ("wallpicture", new Vector3 (0f, 180f, 103.033f));
		officeRot.Add ("chair6", new Vector3 (2.1733f, 180f, 120f));
		officeRot.Add ("officechair", new Vector3 (270f, 40f, 0f));
		officeRot.Add ("L&Jposter", new Vector3 (0f, 270f, 70.6294f));
		officeRot.Add ("bluechair1", new Vector3 (403.57f, 237.87f, -136.37f));
		officeRot.Add ("trashbasket", new Vector3 (350.31f, -180f, -180f));
		officeRot.Add ("cup1", new Vector3 (270f, 0f, 0f));
		officeRot.Add ("phone_2", new Vector3 (270f, 54.693f, 0f));
		officeRot.Add ("bookletstack", new Vector3 (0f, 0f, 0f));
		officeRot.Add ("folders", new Vector3(12.78f, -74.62f, 185.80f));

		//All apartment Positions
		apartmentPos.Add ("loveseat", new Vector3 (-3.3587f, -3.066f, 2.1599f));
		apartmentPos.Add ("plateundertv", new Vector3 (0.088f, -2.766f, -0.092f));
		apartmentPos.Add ("smallbook", new Vector3 (-0.179f, -0.16f, 2.362f));
		apartmentPos.Add ("sidetable", new Vector3 (0.3603f, -3.104f, 2.3413f));
		apartmentPos.Add ("creamybunbox", new Vector3(-0.15f, 1.799f, 0.793f));
		apartmentPos.Add ("longcanvas", new Vector3 (-2.793f, -1.403f, -3.692f));
		apartmentPos.Add ("outsidevase", new Vector3 (1.631f, 0.395f, 12.34f));
		apartmentPos.Add ("knifeblock", new Vector3 (-5.701f, 0.435f, -4.754f));
		apartmentPos.Add ("cereal", new Vector3 (0.003f, 1.795f, -0.009f));
		apartmentPos.Add ("sugar", new Vector3 (0.028f, 1.795f, 0.137f));
		apartmentPos.Add ("chips", new Vector3 (0.032f, 1.797f, 0.285f));
		apartmentPos.Add ("beer", new Vector3(0.032f, 1.8f, 0.448f));
		apartmentPos.Add ("bread", new Vector3(0.026f, 1.807f, 0.631f));
		apartmentPos.Add ("coffeecan", new Vector3(0.079f, 1.807f, 0.801f));
		apartmentPos.Add ("salami", new Vector3(-0.121f, 1.833f, 0.878f));
		apartmentPos.Add ("fruitbowl", new Vector3(3.476f, 1.527f, -1.854f));
		apartmentPos.Add ("table", new Vector3(-0.151f, -0.4871f, -7.345f));
		apartmentPos.Add ("chaira", new Vector3(-0.129f, -0.4877f, -6.908f));
		apartmentPos.Add ("chairb", new Vector3(0.57f, -0.487f, -7.335f));
		apartmentPos.Add ("chairc", new Vector3(-0.108f, -0.487f, -7.823f));
		apartmentPos.Add ("chaird", new Vector3(-0.916f, -0.487f, -7.416f));
		//All apartment Rotations
		apartmentRot.Add("loveseat", new Vector3(0f, 216.81f, 0));
		apartmentRot.Add("smallbook", new Vector3(0.44370f, 241.666f, 269.256f));
		apartmentRot.Add("sidetable", new Vector3(-90f, -180f, 0f)); 
		apartmentRot.Add("creamybunbox", new Vector3(0f, 275.25f, 0f));
		apartmentRot.Add("longcanvas", new Vector3(270f, 539.96f, 0f));
		apartmentRot.Add("cereal", new Vector3(0f, 0f, 0f));
		apartmentRot.Add("sugar", new Vector3(0f, 91.014f, 0f));
		apartmentRot.Add("bagofchips", new Vector3(0f, 90f, 0f));
		apartmentRot.Add("beer", new Vector3(-0.204f, 90f, -0.717f));
		apartmentRot.Add("bread", new Vector3(0f, 271.40f, 0f));
		apartmentRot.Add("coffeecan", new Vector3(0f, 72.950f, 0f));
		apartmentRot.Add("fruitbowl", new Vector3(270f, 0f, 0f));
		apartmentRot.Add("table", new Vector3(270f, 179.84f, 0f));
		apartmentRot.Add("chaira", new Vector3(270f, 180f, 0f));
		apartmentRot.Add("chairb", new Vector3(270f, 268.06f, 0f));
		apartmentRot.Add("chairc", new Vector3(270f, 0f, 0f));
		apartmentRot.Add ("chaird", new Vector3 (270f, 90.466f, 0f));
	}

	public static Vector3 ReturnPos(string name)
	{
		string sceneName = Application.loadedLevelName;
			
		if (sceneName == "Apartment_Scene") {
			return apartmentPos [name];
		} else if (sceneName == "Office_Scene") {
			return officePos [name];
		}
		return new Vector3(0, 0, 0);
	}

	public static Vector3 ReturnRot(string name)
	{
		string sceneName = Application.loadedLevelName;
		
		if (sceneName == "Apartment_Scene") {
			return apartmentRot [name];
		} else if (sceneName == "Office_Scene") {
			return officeRot [name];
		}
		return new Vector3(0, 0, 0);
	}

	public static void SetPos(string name, Vector3 pos)
	{
		string sceneName = Application.loadedLevelName;
		
		if (sceneName == "Apartment_Scene") {
			apartmentPos[name] = pos;
		} else if (sceneName == "Office_Scene") {
			officePos[name] = pos;
		}
	}
	public static void SetRot(string name, Vector3 rot)
	{
		string sceneName = Application.loadedLevelName;
		
		if (sceneName == "Apartment_Scene") {
			apartmentRot[name] = rot;
		} else if (sceneName == "Office_Scene") {
			officeRot[name] = rot;
		}
	}

}

