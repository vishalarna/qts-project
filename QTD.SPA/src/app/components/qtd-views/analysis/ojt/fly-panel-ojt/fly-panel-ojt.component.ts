import { Component, OnInit } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-ojt',
  templateUrl: './fly-panel-ojt.component.html',
  styleUrls: ['./fly-panel-ojt.component.scss'],
})
export class FlyPanelOJTComponent implements OnInit {
  editOjtTime: boolean = false;
  AddOjtTime: boolean = true;
  activeTab: string = 'timeEntry';
  constructor(public flyPanelSrvc: FlyInPanelService) {}

  ngOnInit(): void {}
}
