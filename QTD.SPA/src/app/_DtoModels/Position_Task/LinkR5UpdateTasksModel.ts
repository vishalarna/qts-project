import { Position_HistoryCreateOptions } from "../PositionHistory/PositionHistoryCreateOptions";

export class LinkR5UpdateTasksModel {
  link_TaskIds: Array<string>;
  unlinkAll: boolean;
  position_HistoryCreateOptions: Position_HistoryCreateOptions;
  

  setTasksIdsToLink = (id: string) => {
    if (!this.link_TaskIds) this.link_TaskIds = [];
    let taskId = this.link_TaskIds.filter(dd => dd == id)[0];
    if (!taskId) {
      this.link_TaskIds.push(id);
    }
  }
}
