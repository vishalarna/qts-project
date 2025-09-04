export interface ILA_ProviderVM {
    id: number;
    active: boolean;
    isPriority: boolean;
    name: string;
    providerNumber?:string;
    ilaCount: number;
    isNerc:boolean;
  }

  export class ProviderFilterVM{
    id: number;
    provider: string;
    providerNumber:string;
    checked:boolean;
  }
