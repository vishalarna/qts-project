import { Entity } from "../Entity";
import { ILA } from "../ILA/ILA";
import { MetaILAConfigurationPublishOptions } from "../MetaILAConfigurationPublishOption/MetaILAConfigurationPublishOptions";

export class Meta_ILAMembers_Link extends Entity{
    metaILAID: number;
    ilaid:number;
    metaILAConfigPublishOptionID: number;
    sequenceNumber: number;
    ila: ILA;
    metaILAConfigurationPublishOption: MetaILAConfigurationPublishOptions;
    startDate?:Date;
}