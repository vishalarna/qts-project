import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-icon',
  templateUrl: './icon.component.html',
  styleUrls: ['./icon.component.scss'],
})
export class IconComponent implements OnInit {
  constructor() {}

  /**
   The Icon to be displayed
  */
  @Input()
  icon: string;


  ngOnInit(): void {}
}
