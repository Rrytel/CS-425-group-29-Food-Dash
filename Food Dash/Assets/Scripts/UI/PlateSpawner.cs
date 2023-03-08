using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
	public int spawnFrequency = 5;
	public GameObject platePrefab;

	float timer = 0;
	int iteration = 0;

    // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;

		if (timer > spawnFrequency)
		{
			timer = 0;

			switch (iteration)
			{
				case 0:
					platePrefab.GetComponent <Plate> ().type = FoodTypes.Pizza;
					break;

				case 1:
					platePrefab.GetComponent <Plate> ().type = FoodTypes.Burger;
					break;

				case 2:
					platePrefab.GetComponent <Plate> ().type = FoodTypes.Fries;
					break;

				case 4:
					platePrefab.GetComponent <Plate> ().type = FoodTypes.Drink;
					break;
			}

			Instantiate (platePrefab, this.gameObject.transform);

			iteration += 1;
			
			if (iteration > 4)
			{
				iteration = 0;
			}
		}
    }
}
