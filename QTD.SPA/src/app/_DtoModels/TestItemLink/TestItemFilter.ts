import { Entity } from "../Entity";

export class TestItemFilter extends Entity {
    testId: any;
    testItemTypeIds!: any[]
    taxonomyIds!: any[];
    generate_number: any;
    eoId!: any;
}