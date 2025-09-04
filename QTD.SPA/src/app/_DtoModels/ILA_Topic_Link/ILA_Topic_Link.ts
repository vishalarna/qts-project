import { Entity } from "@models/Entity";
import { ILA } from "@models/ILA/ILA";
import { ILA_Topic } from "@models/ILA_Topic/ILA_Topic";

export class ILA_Topic_Link extends Entity {
  ilaId: string;
  ilaTopicId: string;
  ila: ILA;
  ilA_Topic: ILA_Topic;
}
