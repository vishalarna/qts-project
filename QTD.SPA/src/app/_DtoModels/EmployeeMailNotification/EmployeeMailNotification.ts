export class EmployeeMailNotification{
    id:number;
    person:Array<Person>;
    employeeNumber:string;
    username:string;
    position:string;
    organizationalManager:string;
}

export class Person{
    firstName:string;
    lastName:string;
    image:string;
}