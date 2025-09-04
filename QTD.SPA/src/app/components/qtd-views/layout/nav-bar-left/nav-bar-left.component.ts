import { Component, Input, OnInit } from '@angular/core';
import { CustomClaimTypes } from 'src/app/_Shared/Utils/CustomClaims';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';

@Component({
  selector: 'app-nav-bar-left',
  templateUrl: './nav-bar-left.component.html',
  styleUrls: ['./nav-bar-left.component.scss'],
})
export class NavBarLeftComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {}
}
