import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-power-data',
  templateUrl: './fly-panel-power-data.component.html',
  styleUrls: ['./fly-panel-power-data.component.scss']
})
export class FlyPanelPowerDataComponent implements OnInit {
  @Output() powerData = new EventEmitter<any>();
  powerData_array:any[]=[];
  data_array:any[]=[];


  constructor( public flyPanelSrvc: FlyInPanelService) { }

  ngOnInit(): void {

    this.powerData_array=[
      {id:1,text:'PowerData simulation sample data'},
      {id:2,text:'Generic TQ'},
      {id:3,text:'PowerData simulation 001'},
      {id:4,text:'PowerData simulation data'},
      {id:5,text:'PowerData simulation sample data'},
      {id:6,text:'Generic TQ'},
    ]

  }

  OnClick(){
    this.flyPanelSrvc.close();
    this.powerData.emit(this.data_array);
  }

  OnSelectionChange(i:any){
    
    this.data_array.push(i);
    
  }

}
