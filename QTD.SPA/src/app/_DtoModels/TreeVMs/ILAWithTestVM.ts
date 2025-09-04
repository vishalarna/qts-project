export class ILAWithTestVM{
  id!:any;
  name!:string;
  number!:string;
  active!:boolean;
  tests:TestTreeVM[] = [];
}

export class TestTreeVM{
  id!:any;
  testTitle!:string;
  active!:boolean;
}
