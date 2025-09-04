import { Instance } from "@models/Instance/Instance";
import { Entity } from "../Entity";

export class Client extends Entity {
  name!: string;
  instances : Instance[] = [];
}
