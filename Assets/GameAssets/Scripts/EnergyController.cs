using UnityEngine;
using System.Collections;

public class EnergyController : MonoBehaviour {

	public float currentEnergy;
	public float energyMax;
	public float energyPerTick;
	public float timePerTick;

	private float lastTick;

	public EnergyController(float energyMax, float energyPerTick, float timePerTick, float currentEnergy) {
		this.energyMax = energyMax;
		this.energyPerTick = energyPerTick;
		this.timePerTick = timePerTick;
		this.currentEnergy = currentEnergy;
	}

	void Start() {
		lastTick = Time.time;
	}
	// Update is called once per frame
	void Update () {
		if ((Time.time - lastTick) >= timePerTick) {
			currentEnergy += energyPerTick;
			if (currentEnergy > energyMax) {
				currentEnergy = energyMax;
			}
			lastTick = Time.time;
		}
	}

	public void consumeEnergy(float number) {
		float newCurrentEnergy = currentEnergy - number;
		if (newCurrentEnergy < 0) {
			throw new EnergyException ("Not enough energy !");
		}
		currentEnergy = newCurrentEnergy;
	}

}
