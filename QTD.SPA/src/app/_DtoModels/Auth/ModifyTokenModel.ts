export class ModifyTokenModel {
  username!: string;
  setInstanceName!: string | null;
  setUserName!: string | null;

  constructor(userName:string){
    this.username = userName;
  }
}
