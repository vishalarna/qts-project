import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-task-requalification',
  templateUrl: './task-requalification.component.html',
  styleUrls: ['./task-requalification.component.scss']
})
export class TaskRequalificationComponent implements OnInit,OnDestroy {

  constructor() { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    
    localStorage.removeItem('empNav');
    localStorage.removeItem('filter');
  }

}
