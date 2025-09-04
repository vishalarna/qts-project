
export class ILAUpdateOptions{
  name:string
  nickName:string
  number:string
  description:string
  image:string
  providerId!:any
  topicId!:any
  isSelfPaced:boolean
  isOptional:boolean
  isPublished:boolean
  publishDate?:Date
  deliveryMethodId:any
  hasPilotData:boolean
  isProgramManual:boolean
  submissionDate?:Date
  approvalDate?:Date
  expirationDate?:Date
  uploads!:Uint8Array
  trainingPlan!:string;
  isPubliclyAvailableILA : boolean;
}
