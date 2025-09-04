import { SafetyHazard_Set } from "../SaftyHazard_Set/SafetyHazard_Set";
import { Tool } from "../Tool/Tool";
import { SaftyHazard } from "./SaftyHazard";

export class SaftyHazardWithSet{
  saftyHazard!:SaftyHazard;
  safetyHazard_Sets!:SafetyHazard_Set[];
  tools!:Tool[];
}
