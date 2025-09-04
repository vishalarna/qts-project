import { FileUploadModel } from "./FileUploadModel";

export class ScormUploadAddOptions{
    cbtId:number;
    file: FileUploadModel;

    constructor(cbtId:number,file:FileUploadModel){
        this.cbtId=cbtId;
        this.file=file;
    }
}
