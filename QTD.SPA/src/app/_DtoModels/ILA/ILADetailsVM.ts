
export class ILADetailsVM {
  id!: number;
  providerId!: number;
  deliveryMethodId!: number | null;
  number!: string;
  name!: string;
  nickName!: string;
  image!: string;
  description!: string;
  providerName!: string;
  isSelfPaced!: boolean;
  useForEMP!: boolean;
  isPublished!: boolean;
  isProviderNERC?: boolean | null;
  isMeta!: string;
  cbtRequiredForCourse!: boolean;
  creditHours!: number | null;
  deliveryMethodName!: string;
  status!: string;
  active!: boolean;
  ilaTraineeEvaluationCount?:number;
  isPubliclyAvailable!:boolean;
}

