
export class ILACreateOptions{
  name:string
  nickName:string
  ilaDeliveryMethod!:string
  isAvailableForAllILA!:boolean
  number:string
  description:string
  image:string
  providerId!:string
  isSelfPaced:boolean
  isOptional:boolean
  isPublished:boolean
  publishDate?:Date
  deliveryMethodId?:any
  hasPilotData:boolean
  isProgramManual:boolean
  submissionDate?:Date
  approvalDate?:Date
  expirationDate?:Date
  uploads!:Uint8Array
  effectiveDate!:Date
}
