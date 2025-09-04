import { Entity } from "../Entity";
import { Test } from "../Test/Test";

export class TestReleaseEMPSettings extends Entity{
  // keep adding properties here as needed
  usePreTestAndTest!:boolean;
  preTestRequired!:boolean;
  retakeEnabled!:boolean;
  numberOfRetakes?:number;
  preTestId?:number;
  finalTestId?:number;
  preTest:Test;
  finalTest:Test;
}
