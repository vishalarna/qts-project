import { CBTAvailablity } from "./CBTAvailablity";
import { CBTDueDateInterval } from "./CBTDueDateInterval";

export class CBTCreateOptions{
    cBTRequiredForCource: boolean;
    cBTLearningContractInstructions: string;
    availablity: CBTAvailablity;
    dueDateAmount: number = 1;
    empSettingsReleaseTypeId :string;
    setCbtRequiredForCource(value:boolean){
        this.cBTRequiredForCource = value;
    }

    setCbtLearningInstructions(instructions: string){
        this.cBTLearningContractInstructions = instructions;
    }
    setCbtAvailability(value:CBTAvailablity){
        this.availablity = value;
    }

    setCbtDueDateAmount(dateAmount: number){
        this.dueDateAmount = dateAmount;
    }
    setEmpSettingsReleaseTypeId(dueDateTypeId: string){
        this.empSettingsReleaseTypeId = dueDateTypeId;
    }
}
