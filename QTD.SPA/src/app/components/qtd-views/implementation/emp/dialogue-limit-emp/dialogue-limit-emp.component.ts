import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-dialogue-limit-emp',
  templateUrl: './dialogue-limit-emp.component.html',
  styleUrls: ['./dialogue-limit-emp.component.scss']
})
export class DialogueLimitEmpComponent implements OnInit 
{
@Input() licenseNum;
@Output() close = new EventEmitter<any>();
  constructor() { }

  ngOnInit(): void {
  }
  
}
