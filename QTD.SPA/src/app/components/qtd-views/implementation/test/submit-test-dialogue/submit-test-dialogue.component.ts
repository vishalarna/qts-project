import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-submit-test-dialogue',
  templateUrl: './submit-test-dialogue.component.html',
  styleUrls: ['./submit-test-dialogue.component.scss']
})
export class SubmitTestDialogueComponent implements OnInit {
  @Output() close = new EventEmitter<any>();
  @Output() save = new EventEmitter<any>();
  @Input() testType = new EventEmitter<any>();
  
  constructor() { }

  ngOnInit(): void {
    
  }

}

