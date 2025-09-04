export class EmployeeDocumentOptions {
  employeeID!:any;
  uploadFiles!:uploadFileEmployee[];
}

class uploadFileEmployee {
  fileName!: string;
  fileSize!: number;
  fileType!: string;
  fileAsBase64!: string;
}