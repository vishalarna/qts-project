export class SubmitProcedureReviewDto{
    procedureReviewId:string;
    response: string|null;
    comments: string;

    constructor(id: string, res: string | null, comment: string){
        this.procedureReviewId = id;
        this.response = res;
        this.comments = comment;
    }
}