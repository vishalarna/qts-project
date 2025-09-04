import { Entity } from "../Entity";

export class Task_CollaboratorInvitaiton extends Entity{
  invitedByEId!: any;
  invitedForTaskId!: any;
  inviteeEId!: any;
  inviteeEmail!: string;
  inviteDate!: any;
  message!: string;
}
