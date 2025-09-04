export class SegmentObjectiveOrderVM {
    segmentObjectiveLinkId: string; 
    order: number; 
    type: string; 
    objectiveId : string | null; 
  
    constructor(
      _segmentObjectiveLinkId: string,
      _order: number,
      _type: string,
      _objectiveId: string | null
    ) {
      this.segmentObjectiveLinkId = _segmentObjectiveLinkId;
      this.order = _order;
      this.type = _type;
      this.objectiveId = _objectiveId;
    }
  }
  