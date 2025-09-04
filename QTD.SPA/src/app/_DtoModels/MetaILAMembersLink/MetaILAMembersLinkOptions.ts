export class MetaILAMembersLinkOptions{
  metaILAID:any;
  iLAID:any;
  metaILAConfigPublishOptionID:any;
  sequenceNumber:number;
  startDate?: Date;
  }

  export class MetaILAMembersListOptions{
    ilaMetaILAMembers:MetaILAMembersLinkOptions[] = [];
  }
