import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-training-program',
  templateUrl: './training-program.component.html',
  styleUrls: ['./training-program.component.scss']
})
export class TrainingProgramComponent implements OnInit {
  url : string = 'Design and Development / Training Programs'
  constructor() { }

  ngOnInit(): void {
  }

}
