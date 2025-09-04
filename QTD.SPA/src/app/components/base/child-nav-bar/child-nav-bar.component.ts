import { Component, Input, OnInit } from '@angular/core';
import { NavBarMenuItem } from 'src/app/_DtoModels/NavBarMenuItem';

@Component({
  selector: 'app-child-nav-bar',
  templateUrl: './child-nav-bar.component.html',
  styleUrls: ['./child-nav-bar.component.scss']
})
export class ChildNavBarComponent implements OnInit {
  @Input()
  title: string;

  @Input()
  RoutePath: string;

  @Input()
  Data:Array<any>=[
    {
      "title": '',
      "routePath": ''
    }
  ];
  selectedItem: string;
  constructor() { }

  ngOnInit(): void {
    
    
  }

}
