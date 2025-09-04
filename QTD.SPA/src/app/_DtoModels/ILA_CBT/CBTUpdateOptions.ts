import { CBT } from "./CBT";
import { CBTAvailablity } from "./CBTAvailablity";
import { CBTCreateOptions } from "./CBTCreateOptions";
import { CBTDueDateInterval } from "./CBTDueDateInterval";

export class CBTUpdateOptions extends CBTCreateOptions {
    changeDueDate: boolean;

    constructor(cBTRequiredForCource: boolean,
        cBTLearningContractInstructions: string,
        availablity: CBTAvailablity,
        dueDateAmount: number,
        empSettingsReleaseTypeId :string,
        changeDueDate: boolean) {
            super();
            this.cBTRequiredForCource = cBTRequiredForCource;
            this.cBTLearningContractInstructions = cBTLearningContractInstructions;
            this.availablity = availablity;
            this.dueDateAmount = dueDateAmount;
            this.empSettingsReleaseTypeId = empSettingsReleaseTypeId;
            this.changeDueDate = changeDueDate;
    }

    setChangeDueDate(dueDateAmount :number, dueDateTypeId:string){
      this.dueDateAmount = dueDateAmount;
      this.empSettingsReleaseTypeId = dueDateTypeId;
      this.changeDueDate = true;
    }
}
