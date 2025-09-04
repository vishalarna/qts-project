export class ILA_UploadOptions {
  iLAId!: any;
  uploadFiles!:uploadFile[];

  constructor() {
    this.uploadFiles = [];
  }
}

class uploadFile {
  fileName!: string;
  fileSize!: number;
  fileType!: string;
  fileAsBase64!: string;
}
